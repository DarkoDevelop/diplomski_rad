using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

	public Text tekstScore;

	void Update(){
		tekstScore.text = PlayerMovement.scoreScore.ToString();
	}
}
