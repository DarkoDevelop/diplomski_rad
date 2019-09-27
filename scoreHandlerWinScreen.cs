using UnityEngine.UI;
using UnityEngine;

public class scoreHandlerWinScreen : MonoBehaviour {

	public Text tekstScore;
	// Update is called once per frame
	void Update () {
		tekstScore.text = PlayerMovement.scoreScore.ToString();
	}

	public void ExitGame(){
		Application.Quit ();
	}
	public void Mainmenu(){
		Application.LoadLevel (0);
	}
}
