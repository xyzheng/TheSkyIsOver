using UnityEngine;
using System.Collections;

public class rotateTiles : MonoBehaviour {

	public Tile rotation;

	void Start () {
		rotation = GetComponent<Tile>();
	}

	void OnMouseDown () {
		transform.Rotate (new Vector3 (0f, 0f, 90f));
		if (rotation.angle == Tile.Angle.ROTATE_0) {
			rotation.angle = Tile.Angle.ROTATE_90;
		}
		else if (rotation.angle == Tile.Angle.ROTATE_90) {
			rotation.angle = Tile.Angle.ROTATE_180;
		}
		else if (rotation.angle == Tile.Angle.ROTATE_180) {
			rotation.angle = Tile.Angle.ROTATE_270;
		}
		else {
			rotation.angle = Tile.Angle.ROTATE_0;
		}
	}
}