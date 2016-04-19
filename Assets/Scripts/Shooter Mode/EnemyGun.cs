using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

	public GameObject enemyBullet;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("catwithoutarms");
		InvokeRepeating ("FireEnemyBullet", 0, 1f);
	}

	void FireEnemyBullet(){

		if (player != null) { // if player is not dead
			GameObject bullet = (GameObject)Instantiate(enemyBullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
			//bullet.transform.position += new Vector3 (1f, 0f, 0f);
			//bullet.transform.position = transform.position;

			//Vector2 direction = player.transform.position - bullet.transform.position;

			//bullet.GetComponent<EnemyBullet>().SetDirection(direction);

		}
	
	}
}
