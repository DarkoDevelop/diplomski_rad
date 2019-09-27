
using UnityEngine;
using System;

public class Example : MonoBehaviour
{
	//public BeatDetectionFinal procesor;

	void Start ()
	{
		//Instance of AudioProcessor and reference
		BeatDetectorFinal processor = FindObjectOfType<BeatDetectorFinal> ();
		//processor = FindObjectOfType<BeatDetectorFinal> ();
		//processor = beatDetector;
		processor.onBeat.AddListener (onOnbeatDetected);
	}

	void onOnbeatDetected ()
	{
		Debug.Log ("Beat!!!");
	}

}
