using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(AudioSource))]
public class BeatDetectorFinal : MonoBehaviour {


    public AudioSource audioSource;
    float[] his_Buff = new float[43];
	private bool check = false;
	private float time = .2f;
	float[] rightChanell;
	float[] leftChanell;

	//timer for delaying beats 

	float startTime = 0f;
	public float waitFor = .65f;
	bool timerStart = false;

	[Header ("Event")]
	public OnBeatEventHandler onBeat;

    void Update () {
 
        //Instant sound energy for L R - 0 1
		rightChanell = audioSource.GetComponent<AudioSource>().GetSpectrumData(1024, 0, FFTWindow.Hamming);
		leftChanell = audioSource.GetComponent<AudioSource>().GetSpectrumData(1024, 1, FFTWindow.Hamming);

        float e = addChannels (leftChanell, rightChanell);

        //Local average sound energy - E
        float E = sumLocEnergy ()/his_Buff.Length; 

        //calculate variance
        float sumV = 0;
        for (int i = 0; i< 43; i++)
                sumV += (his_Buff[i]-E)*(his_Buff[i]-E);
       
        float V = sumV/his_Buff.Length;
		//Const c
        float constant = (float)((-0.0025714 * V) + 1.5142857);
		// Array for values
        float[] shiftingHistoryBuffer = new float[his_Buff.Length]; 

        for (int i = 0; i<(his_Buff.Length-1); i++) { // Shift the array one slot right
                shiftingHistoryBuffer[i+1] = his_Buff[i]; // Filling the empty slot with the new instant sound energy
        }

        shiftingHistoryBuffer [0] = e;

        for (int i = 0; i<his_Buff.Length; i++) {
                his_Buff[i] = shiftingHistoryBuffer[i]; //Returning the values to the original array
        }

			if ((e > (constant * E)) && check) {   
				check = false;
			} else if (((e < (constant * E)) && !check) && (Time.time - startTime > waitFor) ) {
				//Debug.Log ("Beat");
				check = true;
				startTime = Time.time;
				onBeat.Invoke ();
			} 
 }  
 //Update done
	[System.Serializable]
	public class OnBeatEventHandler : UnityEngine.Events.UnityEvent
	{

	}

    float addChannels(float[] ch_1, float[] ch_2) {
            float e = 0;
            for (int i = 0; i<ch_1.Length; i++) {
                    e += ((ch_1[i]*ch_1[i]) + (ch_2[i]*ch_2[i]));
            }
            return e;
    }

    float sumLocEnergy() {
            float E = 0;

            for (int i = 0; i<his_Buff.Length; i++) {
                    E += his_Buff[i]*his_Buff[i];
            }
            return E;
    }
}
