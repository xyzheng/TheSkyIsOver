using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public enum TileType { BOTTOM_RIGHT, THREE_WAY, CROSS, LEFT_RIGHT, LEFT_TOP1, LEFT_TOP2, LEFT_TOP3, LEFT_TOP4, TOP_DOWN, TOP_RIGHT }
	//public int GameObjectState;

	public GameObject bottomRight;
	public GameObject cross;
	public GameObject leftRight;
	public GameObject leftTop1;
	public GameObject leftTop2;
	public GameObject leftTop3;
	public GameObject leftTop4;
	public GameObject topDown;
	public GameObject topRight;
	GameObject [][] Tiles;

	// Use this for initialization
	void Start () {
		generateLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void handleInput () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D clickCollider = Physics2D.OverlapPoint (mousePosition);
			if (clickCollider) {

			}
		}
	}

	void generateLevel () {
		for (int i=0; i<10; i+=2){
			for (int j=0; j<10; j+=2){
				if ((i == 8 && j == 8) || (i == 0 && j == 0)) {
					
				}
				int randomGameObjectNum = Random.Range (1, 10);
				if (randomGameObjectNum == 1) {
					Instantiate (bottomRight, new Vector3 (i, j, 0), Quaternion.identity);
				}
				//if (randomGameObjectNum == 2 && !((i == 8 && j == 8) || (i == 0 && j == 0))) {
				if (randomGameObjectNum == 2) {
					Instantiate (cross, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 3) {
					Instantiate (leftRight, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 4) {
					Instantiate (leftTop1, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 5) {
					Instantiate (leftTop2, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 6) {
					Instantiate (leftTop3, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 7) {
					Instantiate (leftTop4, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 8) {
					Instantiate (topDown, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomGameObjectNum == 9) {
					Instantiate (topRight, new Vector3 (i, j, 0), Quaternion.identity);
				}
				
			}
		}
	}
}
