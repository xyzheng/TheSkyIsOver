using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject shooterGM;
	public ShooterGameManager sgm;

	float maxSpawnRate = 5f;

	public GameObject enemy;
	public GameObject enemy2;

	//use percentages to determine how often each one spawns as time goes on
	float enemyOnePercent;
	float enemyTwoPercent;

	// Use this for initialization
	void Start () {
		Invoke ("SpawnEnemy", 1f); 

		// incrase spawn rate every 30 seconds
		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnEnemy(){
	
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		GameObject anEnemy;

		int randomEnemy = Random.Range (0, 2);
		if (randomEnemy == 0) {
			anEnemy = (GameObject)Instantiate (enemy);
		}
		else {
			anEnemy = (GameObject)Instantiate (enemy2);
		}

		//spawn either left or right side
		int randomSide = Random.Range (0, 2);
		if (randomSide == 0) {
			anEnemy.transform.position = new Vector2 (min.x + 0.5f, Random.Range(min.y, max.y));
		}
		else {
			anEnemy.transform.position = new Vector2 (max.x - 0.5f, Random.Range(min.y, max.y));
		}
	
		// schedule when to spawn next enemy

		ScheduleNextEnemySpawn ();

	}

	void ScheduleNextEnemySpawn(){
	
		float spawnInNSeconds;

		if (maxSpawnRate > 1f) {
			// pick a number between 1 and maxSpawnRate
			spawnInNSeconds = Random.Range (1f, maxSpawnRate);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemy", spawnInNSeconds);
	}

	// to increase difficulty

	void IncraseSpawnRate(){
		if (maxSpawnRate > 1f) {
			maxSpawnRate--;
		}
		if (maxSpawnRate == 1f) {
			CancelInvoke("IncreaseSpawnRate");
		}
	}
}
