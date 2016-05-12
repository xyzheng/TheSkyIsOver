using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;
	public int level = 0;

	public bool solvedInTime;
	public int hpLeft;

	void Awake () {
		//loads into the game, if already exists, delete
		if (instance) {
			DestroyImmediate (gameObject);
		}
		else {
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
//		Debug.Log (level);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (level);
	}

	void OnLevelWasLoaded (int levelNum) {
		if (Application.loadedLevelName == "Puzzle") {
			level++;
		}
		if (Application.loadedLevelName == "Menu") {
			level = 0;
		}
	}
}
