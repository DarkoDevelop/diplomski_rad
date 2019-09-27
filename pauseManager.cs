using UnityEngine.UI;
using UnityEngine;

public class pauseManager : MonoBehaviour {

	public AudioSource soundSource;
	public GameObject panelPaused, pausedPaused, continuePaused, menuPaused, exitPaused;
	// Use this for initialization
	void Start () {
		panelPaused.SetActive(false);
		pausedPaused.SetActive(false);
		continuePaused.SetActive(false);
		menuPaused.SetActive(false);
		exitPaused.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("PausedUpdate");
		if (Input.GetButton ("Pause")) {
			soundSource.Pause ();
			panelPaused.SetActive(true);
			pausedPaused.SetActive(true);
			continuePaused.SetActive(true);
			menuPaused.SetActive(true);
			exitPaused.SetActive(true);
			Time.timeScale = 0;
		}
			
	}

	public void buttonContinue(){
		soundSource.Play ();
		Time.timeScale = 1;
		panelPaused.SetActive(false);
		pausedPaused.SetActive(false);
		continuePaused.SetActive(false);
		menuPaused.SetActive(false);
		exitPaused.SetActive(false);
	}

	public void buttonMenu(){
		Time.timeScale = 1;
		Application.LoadLevel (0);
	}
	public void buttonQuit(){
		Application.Quit ();
	}
}
