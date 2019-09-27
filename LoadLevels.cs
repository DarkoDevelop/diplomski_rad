using UnityEngine;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour {


	public void LoadLevel0(){
		Application.LoadLevel (0);

	}
		
	public void LoadLevel1(){
		Application.LoadLevel (1);
	}

	public void LoadLevel2(){
		Application.LoadLevel (2);
	}

	public void LoadLevel3(){
		Application.LoadLevel (3);
	}

	public void LoadLevel4(){
		Application.LoadLevel (4);
	}

	public void LoadLevel5(){
		Application.LoadLevel (5);
	}

	public void ExitGame(){
		Application.Quit ();
	}
		
}
