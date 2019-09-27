using UnityEngine;

public class jumpBoxCollider : MonoBehaviour {
	
	//public bool enterJumpSection = false;
	//public PlayerMovement plMovement;
	//public KeyCode myKey;
	//public GameObject thisInstance;
	//public GameObject effect;
	public PlayerMovement player;
	//public GameObject thisInstance;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<PlayerMovement>();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		//Debug.Log ("skocio");
		//if (col.CompareTag ("Player")) {
		//	myKey = KeyCode.Space;
			//enterJumpSection = true;
		//}
		if (col.CompareTag ("Player")) {
			player.Jump ();
			//Explosion ();
			//Destroy (thisInstance);
		}
	}

	//void onTriggerExit2D(Collider2D col){
	//	if (col.CompareTag ("Player")) {
	//		player.endTriggers ();
	//
	//	}
	//	Debug.Log ("Izaso iz jumpa ");
	//}

	//void Explosion(){
		//Instantiate (effect, transform.position, transform.rotation);

	//}

}
	