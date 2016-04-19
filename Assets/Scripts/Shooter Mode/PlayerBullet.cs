﻿using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public float speed = 3f;

	// Update is called once per frame
	void Update () {
	
		Vector2 position = transform.position;
		position = new Vector2(position.x + speed * Time.deltaTime, position.y);
		transform.position = position;

		// if the bullet goes outside the screen

		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		if ((transform.position.x > max.x) || (transform.position.x < min.x)) {
		
			Destroy (gameObject);
		
		}

	}
}
