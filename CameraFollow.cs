﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float smoothTimeX;
	public float smoothTimeY = .2f;
	public float offsetX = .5f;
	public float offsetY = .3f;
	public GameObject player;

	private Vector2 velocity;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () {
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX + offsetX, posY - offsetY, transform.position.z);
	}
}
