using UnityEngine.UI;
using UnityEngine;

public class rewindHandler : MonoBehaviour {

	public Text tekstScore;
	double a;

	void Update(){
		a = System.Math.Round (PlayerMovement.recordTime, 2);
		if(a > 0)
			tekstScore.text = a.ToString() + " s";
		else 
			tekstScore.text = "0 s";
	}
}
