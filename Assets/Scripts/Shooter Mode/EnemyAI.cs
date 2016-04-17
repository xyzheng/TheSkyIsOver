﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	public float moveSpeed = 1f;
	public float rotationSpeed = 1f;
	
	void Start() 
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void Update() 
	{    
		

		if (target != null) {
			Vector2 dir = target.position - transform.position;
			if (dir != Vector2.zero) 
				transform.rotation = Quaternion.Slerp (transform.rotation, 
				                                       Quaternion.FromToRotation (Vector2.right, dir), 
				                                       rotationSpeed * Time.deltaTime);
			
			//Move Towards Target
			transform.position += (target.position - transform.position).normalized 
				* moveSpeed * Time.deltaTime;
		} 
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			Destroy (gameObject);
		}
	}
}