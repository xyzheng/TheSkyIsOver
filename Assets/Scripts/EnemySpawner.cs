using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	float maxSpawnRate = 5f;

	public GameObject enemy;
	int enemiesToSpawn = 6;

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		//if (gameManager.GetComponent<GameManagement>().state == GameManagement.gameState.PLAY) {
			Invoke ("SpawnEnemy", 1f); 
		//}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnEnemy(){
		
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

			GameObject anEnemy = (GameObject)Instantiate (enemy);
			anEnemy.transform.position = new Vector2 (min.x, Random.Range(min.y, max.y));
		
			// schedule when to spawn next enemy

			if (enemiesToSpawn > 0) {
				ScheduleNextEnemySpawn ();
				enemiesToSpawn -= 1;
			}

	}

	void ScheduleNextEnemySpawn(){
		//if (gameManager.GetComponent<GameManagement>().state == GameManagement.gameState.PLAY) {
			float spawnInNSeconds;

			if (maxSpawnRate > 1f) {
				// pick a number between 1 and maxSpawnRate
				spawnInNSeconds = Random.Range (1f, maxSpawnRate);
			} else {
				spawnInNSeconds = 1f;
			}

			Invoke ("SpawnEnemy", spawnInNSeconds);
		}
	//}
}
