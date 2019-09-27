using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColour : MonoBehaviour {
	public float  r = 0f;
	public float  g = 0f;
	public float  b = 0f;
	public Renderer rend;
	// Use this for initialization
	void Awake () {
		// Instance of PlayerMovement
		PlayerMovement changeCol = FindObjectOfType<PlayerMovement> ();
		changeCol.onBeatChange.AddListener (changeObjectColour);
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
	}
	void Update(){
		r = Random.Range (0f, 1f);
		g = Random.Range (0f, 1f);
		b = Random.Range (0f, 1f);
	}

	void changeObjectColour(){
		rend.material.color = new Color (r,g,b);
	}
}
