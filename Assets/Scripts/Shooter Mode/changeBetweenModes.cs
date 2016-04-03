using UnityEngine;
using System.Collections;

public class changeBetweenModes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			if (Application.loadedLevelName == "Shooter") {
				Application.LoadLevel ("Puzzle");
			}
			else {
				Application.LoadLevel ("Shooter");
			}
		}
	}
}
