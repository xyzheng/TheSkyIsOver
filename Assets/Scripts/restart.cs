using UnityEngine;
using System.Collections;

public class restart : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel ("Puzzle");
		}
	}
}
