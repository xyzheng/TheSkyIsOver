using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject oneWay;
	public GameObject twoWay;
	public GameObject threeWay;
	public GameObject cross;

	public GameObject[][] tiles;

	// Use this for initialization
	void Start () {
		tiles = new GameObject[5][];
		for (int i=0; i<5; i++) {
			tiles[i] = new GameObject[5];
		}
		generateLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			clearTiles();
			generateLevel();
		}
		check();
	}
		
	public void check () {
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				//if tile is a one way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY) {
					//if tile is rotated 0 degrees or 180
					if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) {
						//check top
						if (checkTop (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (checkBottom (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					//if tile is rotated 90 degrees or 270
					else {
						//check left
						if (checkLeft (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (checkRight (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
				}

				//if tile is a two way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY) {
					//if tile is rotated 0 degrees 
					if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0) {
						//check top 
						if (checkTop (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (checkRight (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90) {
						//check top
						if (checkTop (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check left
						if (checkLeft (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) {
						//check left
						if (checkLeft (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (checkBottom (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else {
						//check bot
						if (checkBottom (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (checkRight (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
				}

				//if tile is a three way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY) {
					//if tile is rotated 0 degrees
					if (tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_0) {
						//check top
						if (checkTop (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (checkRight (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (checkBottom (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_90) {
						//check right
						if (checkRight (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check top
						if (checkTop (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check left
						if (checkLeft (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) {
						//check bot
						if (checkBottom (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check left
						if (checkLeft (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check top
						if (checkTop (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else {
						//check left
						if (checkLeft (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (checkBottom (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (checkRight (i, j)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
				}

				//if tile is a cross road
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.CROSS) {
					//check top
					if (checkTop (i, j)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
					//check right
					if (checkRight (i, j)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
					//check bot
					if (checkBottom (i, j)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
					//check left
					if (checkLeft (i, j)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
				}
			}
		}
	}

	bool checkTop (int x, int y) {
		return (y < 4 && tiles[x][y+1] && tiles[x][y+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
			((tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
				(tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
					tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.CROSS));
	}
		
	bool checkLeft (int x, int y) {
		return (x > 0 && tiles[x-1][y] && tiles[x-1][y].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
			((tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
					tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.CROSS));
	}

	bool checkBottom (int x, int y) {
		return (y > 0 && tiles[x][y-1] && tiles[x][y-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
			((tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
				(tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
				(tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
					tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.CROSS));
			
	}

	bool checkRight (int x, int y) {
		return (x < 4 && tiles[x+1][y] && tiles[x+1][y].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
			((tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
				(tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
					tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.CROSS));
	}

	//destroy all instantiated objects
	void clearTiles () {
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				Destroy (tiles[i][j]);
				tiles[i][j] = null;
			}	
		}
	}

	//randomly place tiles in different locations of the grid with different rotations
	void generateLevel () {
		for (int i=0; i<5; i++){
			for (int j=0; j<5; j++){
				int randomGameObjectNum = Random.Range (1, 4);
				//for one-way tiles
				if (randomGameObjectNum == 1) {
					int rotateAngle = Random.Range (1, 3); 
					if (rotateAngle == 1) {
						//GameObject tile = (GameObject)Instantiate (oneWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 90f));
						tiles[i][j] = (GameObject)Instantiate (oneWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 90f));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_90;
					}
					else {
						tiles[i][j] = ((GameObject)Instantiate (oneWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 0f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_0;
					}
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.ONE_WAY;
					//start and end color change to yellow
					if ((i == 0 && j == 0) || (i == 4 && j == 4)) {
						tiles[i][j].GetComponent<Tile>().connect();
					//	tiles[x][y].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
				}

				//for two-way tiles
				else if (randomGameObjectNum == 2) {
					int rotateAngle = Random.Range (1, 5); 
					if (rotateAngle == 1) {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 90f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_90;
					}
					else if (rotateAngle == 2) {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 180f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_180;
					}
					else if (rotateAngle == 3) {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 270f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_270;
					}
					else {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 0f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_0;
					}
					//start and end color change to yellow
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.TWO_WAY;
					if ((i == 0 && j == 0) || (i == 4 && j == 4)) {
						tiles[i][j].GetComponent<Tile>().connect();
					//	tiles[x][y].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
				}

				//for three-way tiles
				else if (randomGameObjectNum == 3) {
					int rotateAngle = Random.Range (1, 5); 
					if (rotateAngle == 1) {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 90f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_90;
					}
					else if (rotateAngle == 2) {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 180f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_180;
					}
					else if (rotateAngle == 3) {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 270f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_270;
					}
					else {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 0f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_0;
					}
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.THREE_WAY;
					//start and end color change to yellow
					if ((i == 0 && j == 0) || (i == 4 && j == 4)) {
						tiles[i][j].GetComponent<Tile>().connect();
					//	tiles[x][y].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
				}

				//for cross tiles
				else {// (randomGameObjectNum == 4) {
					tiles[i][j] = ((GameObject)Instantiate (cross, new Vector3 (i, j, 0), Quaternion.identity));
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.CROSS;
					//start and end color change to yellow
					if ((i == 0 && j == 0) || (i == 4 && j == 4)) {
						tiles[i][j].GetComponent<Tile>().connect();
					//	tiles[x][y].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
				}
			}
		}
	}
}
