using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public enum TileType { ONE_WAY, TWO_WAY, THREE_WAY, CROSS }
	public enum Angle { ROTATE_0, ROTATE_90, ROTATE_180, ROTATE_270 }
	public enum Connection { NOT_CONNECTED, CONNECTED }

	public TileType type;
	public Angle angle;
	public Connection connectedState;
	public bool startTile;
	public bool connectedToTileConnectedToStart;

	public GameObject gameManager;
	public GameManager gmScript;

	public Tile top;
	public Tile right;
	public Tile bot; 
	public Tile left;

	public int positionX;
	public int positionY;

	SpriteRenderer rend;

	void Start () {
		connectedToTileConnectedToStart = false;
		rend = GetComponent<SpriteRenderer>();

		gameManager = GameObject.Find ("Game Manager");
		gmScript = gameManager.GetComponent<GameManager>();
		positionX = (int)transform.position.x;
		positionY = (int)transform.position.y;

		//set gameobjects of top/right/bot/left
		if (positionY <= 3) {
			top = gmScript.tiles[positionX][positionY + 1].GetComponent<Tile>();
		}
		if (positionY >= 1) {
			bot = gmScript.tiles[positionX][positionY - 1].GetComponent<Tile>();
		}
		if (positionX <= 3) {
			right = gmScript.tiles[positionX + 1][positionY].GetComponent<Tile>();
		}
		if (positionX >= 1) {
			left = gmScript.tiles[positionX - 1][positionY].GetComponent<Tile>();
		}

	}

	void Update () {
		if (gmScript.connectedTiles.Contains(transform.gameObject)) {
			connectedState = Connection.CONNECTED;
			rend.color = Color.yellow;
		}
		else {
			if (transform.position.x != 4 && transform.position.y != 4) {
				rend.color = Color.white;
			}
			connectedState = Connection.NOT_CONNECTED;
		}
	}

	public void connect () {
		SpriteRenderer rend = GetComponent<SpriteRenderer>();
		rend.color = Color.yellow;
		connectedState = Connection.CONNECTED;
	}

	public void disconnect () {
		SpriteRenderer rend = GetComponent<SpriteRenderer>();
		rend.color = Color.white;
		connectedState = Connection.NOT_CONNECTED;
	}

	public void checkLeft () {
		//return (left.startTile || left.connectedToTileConnectedToStart
	}

	public void checkRight () {

	}

	public void checkTop () {

	}

	public void checkBottom () {

	}
}