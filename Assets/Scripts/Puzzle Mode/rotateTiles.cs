using UnityEngine;
using System.Collections;

public class rotateTiles : MonoBehaviour {

	public Tile rotation;
	public float turnRate = 100f;
	private Quaternion destination = Quaternion.identity;
	private bool isRotating;

	void Start () {
		isRotating = false;
		destination = Quaternion.Euler (transform.eulerAngles);
		rotation = GetComponent<Tile>();
	}

	void Update () {
		transform.rotation = Quaternion.RotateTowards (transform.rotation, destination, turnRate * Time.deltaTime);
	}

	void OnMouseDown () {
		if (!isRotating) {
		//transform.Rotate (new Vector3 (0f, 0f, 90f));
			if (rotation.angle == Tile.Angle.ROTATE_0) {
				Vector3 dest = new Vector3 (0f, 0f, 90f);
				SetBlendedEulerAngles (dest);
				rotation.angle = Tile.Angle.ROTATE_90;
			}
			else if (rotation.angle == Tile.Angle.ROTATE_90) {
				Vector3 dest = new Vector3 (0f, 0f, 180f);
				SetBlendedEulerAngles (dest);
				rotation.angle = Tile.Angle.ROTATE_180;
				}
			else if (rotation.angle == Tile.Angle.ROTATE_180) {
				Vector3 dest = new Vector3 (0f, 0f, 270f);
				SetBlendedEulerAngles (dest);
				rotation.angle = Tile.Angle.ROTATE_270;
			}
			else {
				Vector3 dest = new Vector3 (0f, 0f, 0f);
				SetBlendedEulerAngles (dest);
				rotation.angle = Tile.Angle.ROTATE_0;
			}
		}
		isRotating = false;
	}

	//call this to play animation
	public void SetBlendedEulerAngles (Vector3 dest) {
		isRotating = true;
		destination = Quaternion.Euler (dest);
	}

	//animate the rotating tile
	public IEnumerator spinTile (Vector3 dest) {
		Vector3 origin = transform.eulerAngles;
			origin = new Vector3 (Mathf.LerpAngle (origin.x, dest.x, Time.deltaTime),
			Mathf.LerpAngle (origin.y, dest.y, Time.deltaTime),
			Mathf.LerpAngle (origin.z, dest.z, Time.deltaTime));
		transform.eulerAngles = origin;
		//isRotating = true;
		yield return null;
	}
}