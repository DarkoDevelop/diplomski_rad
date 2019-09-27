using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchBoxCollider : MonoBehaviour {


	public PlayerMovement plMovement;

	// Update is called once per frame
	void Start(){
		plMovement = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}


	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			plMovement.Crouch ();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		plMovement.endTriggers ();
		plMovement.endCrouch ();
	}


}
