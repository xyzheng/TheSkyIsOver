using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			if (Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel (0);
			}
		}
	}

}
