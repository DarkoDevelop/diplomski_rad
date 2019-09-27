using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTriggerEnd : MonoBehaviour {

	public PlayerMovement player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerMovement>();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			player.FlyEnd();
			//Debug.Log ("Triggered FlyEnd");
		}
	}
}