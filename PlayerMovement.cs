using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	#region RewindSectionVariables
	public bool isRewinding = false;
	static public float recordTime = 4f;
	List<PointsInTime> pointsInTime;
	private Rigidbody2D rb2d;
	#endregion

	public GameObject pickUpEffect;
	private bool isArrowPressed = false;

	public CharacterController2D controller;
	public jumpBoxCollider jumpboxcollider;
	//public crouchBoxCollider crouchboxcollider;
	Animator anim;
	public Transform player;
	static int i = 0;
	public ParticleSystem flyParticle;
	public ParticleSystem crouchParticle;
	public ParticleSystem runParticle;

	private float horizontalMove;
	public float flyMove = 0f;
	public bool jump = false; 
	public bool crouch = false;
	public bool fly = false;
	public bool walk = false; 
	public bool walk_bool = false; 
	private bool flyCheck = false;

	static public int scoreScore = 0;

	private float RunSpeed = 54f;
	public float flySpeed = 20f;
	public float WalkSpeed = 20f;

	//AudioSource
	public AudioSource audioSong;
	//double sourceStartTime = AudioSettings.dspTime + 2f;
	//double songTime = 0f;

	#region Beat Variables
	//beat variables
	private bool beatDetected = false;
	private float beatTimerTime = 0f;
	private float beatEndTimerTime = 0f;
	private bool beatTimerRunning = false;
	private bool beatDetectedFinal = false;
	#endregion

	//Force for fly 
	public ConstantForce2D force2d;

	//CrouchSection
	private bool crouchPressed = false;
	//public Collider2D CrouchDisableCollider;

	#region Jump code
	//Jump section.. 
	//private bool isJumpsSame = false;
	//private bool spacePressed = false;
	//private KeyCode IsSpace = KeyCode.Space;

	//Timers for Jump
	private float TimerTime = 0f;
	private float KeyPressTime = 0f;
	private bool TimerRunning = false;
	public bool JumpPressed = false;
	#endregion 

	//Run section..
	//private KeyCode isRightArrow = KeyCode.RightArrow;
	//private float isPressedArrow = 0f;

	[Header ("Event")]
	public ChangeColourHandler onBeatChange;


	void Awake(){
		
		force2d = GetComponent<ConstantForce2D>();

	}


	void Start () {

		#region RewindVariables
		pointsInTime = new List<PointsInTime> ();
		rb2d = GetComponent<Rigidbody2D> ();
		#endregion

		scoreScore = 0;
		recordTime = 4f;
		
		//Coordinates of player-for..
		player = GameObject.Find ("Player").GetComponent<Transform> ();

		//Instance of AudioProcessor and reference
		BeatDetectorFinal processor = FindObjectOfType<BeatDetectorFinal> ();

		//processor = beatDetector;

		processor.onBeat.AddListener (onOnbeatDetected);
		//Animator reference
		anim = GetComponent<Animator> ();

		//2D character shadows 
		GetComponent<Renderer>().shadowCastingMode =  UnityEngine.Rendering.ShadowCastingMode.On;
		GetComponent<Renderer>().receiveShadows = true;

	}
	
	// Update is called once per frame
	void Update ()
	{


		#region rewindUpdate
		if (recordTime > 0){
			if (Input.GetButtonDown("Rewind")){
				
				StartRewind();
			}
			if (Input.GetButtonUp("Rewind"))
					StopRewind();
		}else if (recordTime < 0)
			
		if (Input.GetButtonUp("Rewind")){
			
				StopRewind();
		}
		#endregion
	
		#region runParticles
		//runPartticles 
		if(controller.m_Grounded)
			runParticle.enableEmission = true;
		else
			runParticle.enableEmission = false;
		#endregion


		#region spaceDetectionTimer
		//Is space pressed - 1.2 seconds should pass and 
		if(Input.GetKey("space") && !TimerRunning){
			TimerRunning = true;
			KeyPressTime = Time.time;
		}
		if(TimerRunning){
			TimerTime = Time.time - KeyPressTime;
			//Debug.Log (TimerTime);
		}
		if (TimerTime < 1f && TimerRunning) {
			JumpPressed = true;
		} else {
			JumpPressed = false;
			TimerRunning = false;
		}
		//Debug.Log (flyCheck);
		#endregion


		#region beatDetectionTimer
		if(beatDetected && !beatTimerRunning){
			beatTimerRunning = true;
			beatTimerTime = Time.time;
			//Debug.Log (beatTimerTime);
		}
		if(beatTimerRunning){
			beatEndTimerTime = Time.time - beatTimerTime;
		}
		//Debug.Log (beatTimerRunning);
		if (beatEndTimerTime < .3f && beatTimerRunning) {
			beatDetectedFinal = true;
		} else {
			beatDetectedFinal = false;
			beatTimerRunning = false;
		}
		//Debug.Log (beatDetectedFinal);
		#endregion


		#region Run/Walk
		if (controller.m_Grounded && !walk_bool) {
			horizontalMove = 45f;
		} else if(walk_bool)
			horizontalMove = 25f;
		#endregion


		#region Crouch
		if (Input.GetButton ("Crouch")) {;
			crouchPressed = true;
		} else{
			crouchPressed = false;
		}
		if (crouch){
			runParticle.enableEmission = false;
			crouchParticle.enableEmission = true;
		}
		else{
			if(!controller.m_Grounded){
				runParticle.enableEmission = false;
			}
			crouchParticle.enableEmission = false;
		}
		#endregion


		#region Fly
		if (Input.GetButtonDown ("Fly") && !crouch && flyCheck) {
			fly = true;
			flyParticle.enableEmission = true;
		}else if(Input.GetButtonUp ("Fly") || !flyCheck || crouch){
			fly = false;
			flyParticle.enableEmission = false;
		}
		#endregion

		#region Animator
		if (jump && !fly) {
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isJumping", true);
		} else if (controller.m_Grounded && crouch) {
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isCrouching", true);
		} else if (fly && !jump) {
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isFlying", true);
		} else if(walk_bool && controller.m_Grounded){
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isWalking", true);
		} else if(controller.m_Grounded) {
			anim.SetBool ("isJumping", false);
			anim.SetBool ("isFlying", false);
			anim.SetBool ("isCrouching", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isRunning", true);
		}
		#endregion
	

}


	void FixedUpdate(){
		//Debug.Log (fly);
		controller.Move (horizontalMove * Time.fixedDeltaTime, crouch, jump, fly, walk);
		jump = false;
		//force2d.enabled = !force2d.enabled;

		#region RewindFixed

		if (isRewinding && recordTime > 0){
			Rewind ();
			recordTime -= Time.fixedDeltaTime;
		}
		else if (!isRewinding && recordTime > 0)
			Record ();
		else if(recordTime < 0)
			StopRewind();
		
		#endregion

	}

	[System.Serializable]
	public class ChangeColourHandler : UnityEngine.Events.UnityEvent
	{

	}

	public void RunWalk(){
		walk_bool = true;
	}

	public void WalkRun(){
		walk_bool = false;
	}

	public void Jump(){
		if(JumpPressed && beatDetectedFinal){
		jump = true;
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, fly, walk);
			//Score ++
		}
	}
		

	public void Crouch(){
		if (beatDetectedFinal && crouchPressed) {
			crouch = true;
			controller.Move (horizontalMove * Time.fixedDeltaTime, crouch, jump, fly, walk);
		}
	}

	public void endTriggers(){
		addScoreUp ();
	}

	public void endCrouch(){
		crouch = false;
	}

	public void FlyStart(){
		flyCheck = true;
	}

	public void FlyEnd (){
		flyCheck = false;
	}
	public void setArrowPressedTrue(){
		isArrowPressed = true;
	}
	public void setArrowPressedFalse(){
		isArrowPressed = false;
	}


	public void pointsUp(){
		if(isArrowPressed){
		if (i % 2 == 0) {
			//Debug.Log ("30 Points to griffindor");
			//Debug.Log ("tu sam kod 30");
			//pickUpEffect.enableEmission = true;
			isArrowPressed = false;
			Instantiate(pickUpEffect , transform.position, transform.rotation);
			addScoreUp();
			//onBeatChange.Invoke ();
			i++;
			//return;

		} else if (i % 2 == 1)
			i++;
		
		}
	}

	public void addScoreUp(){
		scoreScore += 10;
	}
		

	void onOnbeatDetected ()
	{
		beatDetected = true;
		//in case of first run for setting up space
		//Debug.Log ("Beat!!!");
		//Debug.Log (player.position.x);
		//addScoreUp ();
		onBeatChange.Invoke ();
	}

	 void OnTriggerEnter2D(Collider2D col){
		
			if (col.CompareTag ("StartSong")) {
				audioSong.Play();
			}
			if (col.CompareTag ("Walls")) {
			hitRewind ();
			}
			if (col.CompareTag ("EndGame")) {
				Application.LoadLevel (3);
			}
		
	}

	void OnTriggerStay2D(Collider2D col){

		if (!crouchPressed) {
			if (col.CompareTag ("CrouchHit")) {
				hitRewind ();
			} 
		}
	}
	void OnTriggerExit2D(Collider2D col){

		if (col.CompareTag ("Jump")) {
			endTriggers ();
		}
	}

	#region RewindFunctions

	void hitRewind(){
		if (recordTime > 0)
			StartRewind ();
		else
			Application.LoadLevel (4);
	}

	void Rewind(){
		
		if (pointsInTime.Count > 0) {
			
			PointsInTime pointInTime = pointsInTime[0]; 
			transform.position = pointInTime.position;
			audioSong.time = pointInTime.songTime;
			pointsInTime.RemoveAt (0);

		} else {
			
			StopRewind ();
		}
	}

	void Record (){
		
		if (pointsInTime.Count > Mathf.Round (recordTime / Time.fixedDeltaTime)) {
			pointsInTime.RemoveAt (pointsInTime.Count - 1);

		}

		pointsInTime.Insert (0, new PointsInTime(transform.position , (float)audioSong.time));

	}

	public void StartRewind (){

		//recordTime -= Time.fixedDeltaTime;
		isRewinding = true;
		rb2d.isKinematic = true;

	}

	public void StopRewind(){
		
		isRewinding = false;
		rb2d.isKinematic = false;

	}
		
	#endregion
}
