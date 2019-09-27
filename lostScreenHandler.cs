using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lostScreenHandler : MonoBehaviour {

	public void RestartLevel(){
		Application.LoadLevel (1);
	}
	public void MainMenu(){
		Application.LoadLevel (0);
	}
	public void Exit(){
		Application.Quit ();
	}
}
