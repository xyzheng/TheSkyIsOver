﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject playerBullet;
	public GameObject playerBullet2;
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	Rigidbody2D rbody;
	Animator anim;
	public float moveSpeed = 15f;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
		
			GameObject bullet01 = (GameObject)Instantiate (playerBullet);
			bullet01.transform.position = bulletPosition1.transform.position;

			GameObject bullet02 = (GameObject)Instantiate(playerBullet2);
			bullet02.transform.position = bulletPosition2.transform.position;
		
		}

		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		rbody.MovePosition (rbody.position + movement_vector * moveSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col){
	
		if ((col.tag == "EnemyBullet") || (col.tag == "Enemy")) {
			Destroy (gameObject);
		}
	}
}
