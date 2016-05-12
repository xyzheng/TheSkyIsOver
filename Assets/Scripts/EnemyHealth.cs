using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int enemyHealth;
	GameManagement gameManagement;

	// Use this for initialization
	void Start () {
		gameManagement = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0) {
			Destroy (gameObject);
			gameManagement.enemiesLeft -= 1;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			Destroy (col.gameObject);
			enemyHealth -= 1;
		}
	}



}
