using UnityEngine;
using System.Collections;

public class rotateTiles : MonoBehaviour {
	
	void OnMouseDown () {
		transform.Rotate (new Vector3 (0f, 0f, 90f));
	}
}