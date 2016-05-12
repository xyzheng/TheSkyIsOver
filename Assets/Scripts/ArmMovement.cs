using UnityEngine;
using System.Collections;

public class ArmMovement : MonoBehaviour {

	private float x;
	private Vector2 ls;

	void Start() {
		x = transform.localScale.x;
		ls = transform.localScale;
	}

	void Update () {
		Vector2 dir = new Vector2 (0, Input.GetAxis("Vertical"));

		float rotZ = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;

		if (dir.x >= 0) {
			transform.rotation = Quaternion.Euler  (0f, 0f, rotZ);

			ls.x = x;
			transform.localScale = ls;
		}
		else {
			transform.rotation = Quaternion.Euler  (0f, 0f, rotZ+180);
			ls.x = -x;
			transform.localScale = ls;
		}
	}
}
