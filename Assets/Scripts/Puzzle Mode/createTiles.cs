using UnityEngine;
using System.Collections;

public class createTiles : MonoBehaviour {
	
	public GameObject bottomRight;
	public GameObject cross;
	public GameObject leftRight;
	public GameObject leftTop;
	public GameObject leftTop2;
	public GameObject leftTop3;
	public GameObject leftTop4;
	public GameObject topDown;
	public GameObject topRight;
	
	// Use this for initialization
	void Start () {
		generateLevel ();
	}

	void generateLevel () {
		for (int i=0; i<10; i+=2){
			for (int j=0; j<10; j+=2){
				if ((i == 8 && j == 8) || (i == 0 && j == 0)) {

				}
				int randomTileNum = Random.Range (1, 10);
				if (randomTileNum == 1) {
					Instantiate (bottomRight, new Vector3 (i, j, 0), Quaternion.identity);
				}
				//if (randomTileNum == 2 && !((i == 8 && j == 8) || (i == 0 && j == 0))) {
				if (randomTileNum == 2) {
					Instantiate (cross, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 3) {
					Instantiate (leftRight, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 4) {
					Instantiate (leftTop, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 5) {
					Instantiate (leftTop2, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 6) {
					Instantiate (leftTop3, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 7) {
					Instantiate (leftTop4, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 8) {
					Instantiate (topDown, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (randomTileNum == 9) {
					Instantiate (topRight, new Vector3 (i, j, 0), Quaternion.identity);
				}
				
			}
		}
	}
}