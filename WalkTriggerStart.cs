using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTriggerStart : MonoBehaviour {

	// Use this for initialization

	public PlayerMovement player;

	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerMovement>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			player.RunWalk();
			//Debug.Log ("Triggered RunWalk");
		}
	}
}
