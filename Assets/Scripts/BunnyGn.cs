using UnityEngine;
using System.Collections;

public class BunnyGn : MonoBehaviour {

	public GameObject EnemyBullet;

	PlayerController playerController;
	
	// Use this for initialization
	void Start () {
		Invoke ("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FireEnemyBullet(){
		
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) { // if player is not dead
			GameObject bullet = (GameObject)Instantiate(EnemyBullet);
			
			bullet.transform.position = transform.position;
			
			Vector2 direction = player.transform.position - bullet.transform.position;
			
			bullet.GetComponent<EnemyBullet>().SetDirection(direction);
			
		}
		
	}
}
