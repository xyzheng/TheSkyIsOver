using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public GameObject target;
	public float moveSpeed = 1f;
	public float rotationSpeed = 1f;

	void Start () {
		target = GameObject.Find ("catwithoutarms");
	}

	void Update () {
		if (target != null) {
			//flip x of enemies
			if (transform.position.x < target.transform.position.x) {
				transform.GetComponent<SpriteRenderer>().flipX = true;
			}
			transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
		}

	}
		
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
	}



}
