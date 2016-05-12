using UnityEngine;
using System.Collections;

public class EnemySpawnerRight : MonoBehaviour {

	float maxSpawnRate = 5f;
	
	public GameObject enemy;
	int enemiesToSpawn = 6;

	public GameObject gameManager;
	
	// Use this for initialization
	void Start () {
		Invoke ("SpawnEnemy", 1f); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SpawnEnemy(){
	//	if (gameManager.GetComponent<GameManagement>().state == GameManagement.gameState.PLAY) {
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
			
			GameObject anEnemy = (GameObject)Instantiate (enemy);
			anEnemy.transform.position = new Vector2 (max.x, Random.Range(min.y, max.y));
			
			// schedule when to spawn next enemy
			if (enemiesToSpawn > 0) {
				ScheduleNextEnemySpawn ();
				enemiesToSpawn -= 1;
			}
			
	//	}
	}
	
	void ScheduleNextEnemySpawn(){
	//	if (gameManager.GetComponent<GameManagement>().state == GameManagement.gameState.PLAY) {
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
