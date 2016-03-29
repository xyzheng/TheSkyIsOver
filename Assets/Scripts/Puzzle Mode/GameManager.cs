using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject oneWay;
	public GameObject twoWay;
	public GameObject threeWay;
	public GameObject cross;

	public GameObject[][] tiles;

	//for color purposes later
	//List<GameObject> tiles;

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
						if (j < 4 && tiles[i][j+1] && tiles[i][j+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
								tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
								tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					//if tile is rotated 90 degrees or 270
					else {
						//check left
						if (i > 0 && tiles[i-1][j] && tiles[i-1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
								tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
									tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
				}

				//if tile is a two way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY) {
					//if tile is rotated 0 degrees 
					if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0) {
						//check top 
						if (j < 4 && tiles[i][j+1] && tiles[i][j+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
									tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
									tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90) {
						//check top
						if (j < 4 && tiles[i][j+1] && tiles[i][j+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
									tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check left
						if (i > 0 && tiles[i-1][j] && tiles[i-1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) {
						//check left
						if (i > 0 && tiles[i-1][j] && tiles[i-1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else {
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
									tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
				}

				//if tile is a three way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY) {
					//if tile is rotated 0 degrees
					if (tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_0) {
						//check top
						if (j < 4 && tiles[i][j+1] && tiles[i][j+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
									tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
									tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else if (tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_90) {
						//check right
						if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
									tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check left
					}
					else if (tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) {
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check left
						if (i > 0 && tiles[i-1][j] && tiles[i-1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check top
						if (j < 4 && tiles[i][j+1] && tiles[i][j+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
									tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
					else {
						//check left
						if (i > 0 && tiles[i-1][j] && tiles[i-1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check bot
						if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
								(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
									tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
						//check right
						if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
							((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
								(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
									tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
							tiles[i][j].GetComponent<Tile>().connect();
						}
					}
				}

				//if tile is a cross road
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.CROSS) {
					//check top
					if (j < 4 && tiles[i][j+1] && tiles[i][j+1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
						((tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
							(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
							(tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
								tiles[i][j+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[i][j+1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
					//check right
					if (i < 4 && tiles[i+1][j] && tiles[i+1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
						((tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
							(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
							(tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
								tiles[i+1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i+1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
					//check bot
					if (j > 0 && tiles[i][j-1] && tiles[i][j-1].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
						((tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
							(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
							(tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
								tiles[i][j-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[i][j-1].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
					//check left
					if (i > 0 && tiles[i-1][j] && tiles[i-1][j].GetComponent<Tile>().connectedState == Tile.Connection.CONNECTED && 
						((tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
							(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
							(tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
								tiles[i-1][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[i-1][j].GetComponent<Tile>().type == Tile.TileType.CROSS)) {
						tiles[i][j].GetComponent<Tile>().connect();
					}
				}
			}
		}
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
					//	tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
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
					//	tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
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
					//	tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
				}

				//for cross tiles
				else {// (randomGameObjectNum == 4) {
					tiles[i][j] = ((GameObject)Instantiate (cross, new Vector3 (i, j, 0), Quaternion.identity));
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.CROSS;
					//start and end color change to yellow
					if ((i == 0 && j == 0) || (i == 4 && j == 4)) {
						tiles[i][j].GetComponent<Tile>().connect();
					//	tiles[i][j].GetComponent<Tile>().connectedState = Tile.Connection.CONNECTED;
					}
				}
			}
		}
	}
}
