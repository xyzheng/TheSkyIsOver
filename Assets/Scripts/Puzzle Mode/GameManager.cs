using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject oneWay;
	public GameObject twoWay;
	public GameObject threeWay;
	public GameObject cross;

	//dimensions of board
	private int width;
	private int height;

	public GameObject[][] tiles;

	public List<GameObject> connectedTiles = new List<GameObject>();
	int index;

	// Use this for initialization
	void Start () {
		width = 5;
		height = 5;
		index = 0;
		tiles = new GameObject[width][];
		for (int i=0; i<width; i++) {
			tiles[i] = new GameObject[height];
		}
		generateLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			index = 0;
			clearTiles();
			generateLevel();
		}
		check();

		for (int i=0; i<connectedTiles.Count; i++) {
			connectedTiles[i].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
		}
		/*
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				if (!connectedTiles.Contains(tiles[i][j])) {
					tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.NOT_CONNECTED;
				}
			}
		}
		*/
		Debug.Log (index.ToString() + " " + connectedTiles.Count);
	}
		
	public void check () {
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				//if tile is a one way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY) {
					//if tile is rotated 0 degrees or 180
					if (((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) 
						&& (checkTop (i, j) || checkBottom (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true ) {
							//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					else if (((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)
						&& (checkLeft(i, j) || checkRight(i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true ) {
							//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					// if start tile
					else {
						//connectedTiles.Remove(tiles[i][j]);
						if (i == 0 && j == 0) {

							connectedTiles.RemoveRange(1, connectedTiles.Count - 1);

						}
						/*
						for (int ind=index; ind<connectedTiles.Count; ind++) {
							connectedTiles.RemoveAt(ind);
							index--;
						}
*/
						//if (tiles[i][j].GetComponent<Tile>().startTile != true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.NOT_CONNECTED;
						//}
		
					}
				}

				//if tile is a two way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY) {
					//if tile is rotated 0 degrees && check top or right
					if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 && (checkTop (i, j) || checkRight (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					//if rotated 90 degrees && check top or left
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 && (checkTop (i, j) || checkLeft (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					//if rotated 180 degrees && check left or bot
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 && (checkLeft (i, j) || checkBottom (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					//if rotated 270 degrees && check bot or right
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 && (checkBottom (i, j) || checkRight (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					else {
						//connectedTiles.Remove(tiles[i][j]);
						if (i == 0 && j == 0) {

							connectedTiles.RemoveRange(1, connectedTiles.Count);

						}
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.NOT_CONNECTED;
					}
				}

				//if tile is a three way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY) {
					//if tile is rotated 0 degrees check top, right, bot
					if ((tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_0 && (checkTop (i, j) || checkRight (i, j) || checkBottom (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					//check right, top, left
					else if ((tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_90 && (checkRight (i, j) || checkTop (i, j) || checkLeft (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					//check bot, left, top
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 && (checkBottom (i, j) || checkLeft (i, j) || checkTop (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 && (checkLeft (i, j) || checkBottom (i, j) || checkRight (i, j))) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					else {
						if (i == 0 && j == 0) {

							connectedTiles.RemoveRange(1, connectedTiles.Count);

						}
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.NOT_CONNECTED;
					}
				}

				//if tile is a cross road
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.CROSS) {
					if ((checkTop (i, j) || checkRight (i, j) || checkBottom (i, j) || checkLeft (i, j)) || tiles[i][j].GetComponent<Tile>().startTile == true) {
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						if (!connectedTiles.Contains (tiles[i][j])) {
							connectedTiles.Add (tiles[i][j]);
							index ++;
						}
					}
					else {
						if (i == 0 && j == 0) {

							connectedTiles.RemoveRange(1, connectedTiles.Count);

						}
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.NOT_CONNECTED;
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
		connectedTiles.Clear();
	}

	//randomly place tiles in different locations of the grid with different rotations
	void generateLevel () {
		for (int i=0; i<5; i++){
			for (int j=0; j<5; j++){
				int randomGameObjectNum = Random.Range (1, 5);
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
					if (i == 0 && j == 0) {
						//tiles[i][j].GetComponent<Tile>().connect();
						//tiles[i][j].GetComponent<Tile>().startTile = true;
						connectedTiles.Add (tiles[i][j]);
						index ++;
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
					else if (i == 4 && j == 4) {
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
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
					if ((i == 0 && j == 0)) {
						//tiles[i][j].GetComponent<Tile>().connect();
						tiles[i][j].GetComponent<Tile>().startTile = true;
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						connectedTiles.Add (tiles[i][j]);
						index ++;
					}
					else if (i == 4 && j == 4) {
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
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
					if ((i == 0 && j == 0)) {
						//tiles[i][j].GetComponent<Tile>().connect();
						tiles[i][j].GetComponent<Tile>().startTile = true;
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						connectedTiles.Add (tiles[i][j]);
						index ++;
					}
					else if (i == 4 && j == 4) {
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
					}
				}

				//for cross tiles
				else {// (randomGameObjectNum == 4) {
					tiles[i][j] = ((GameObject)Instantiate (cross, new Vector3 (i, j, 0), Quaternion.identity));
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.CROSS;
					//start and end color change to yellow
					if ((i == 0 && j == 0)) {
						//tiles[i][j].GetComponent<Tile>().connect();
						tiles[i][j].GetComponent<Tile>().startTile = true;
						//tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
						connectedTiles.Add (tiles[i][j]);
						index ++;
					}
					else if (i == 4 && j == 4) {
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
					}
				}
			}
		}
	}
}
