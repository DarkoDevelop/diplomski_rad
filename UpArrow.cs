﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArrow : MonoBehaviour {

	public PlayerMovement player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerMovement>();
	}
	void OnTriggerEnter2D(Collider2D col){
		player.setArrowPressedTrue ();
	}
	void OnTriggerExit2D(Collider2D col){
		player.setArrowPressedFalse ();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && Input.GetButtonUp ("Up")) {
			player.pointsUp();
			//Debug.Log ("Triggered Up");
		}
	}
}