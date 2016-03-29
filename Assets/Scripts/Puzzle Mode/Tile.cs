using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public enum TileType { ONE_WAY, TWO_WAY, THREE_WAY, CROSS }
	public enum Angle { ROTATE_0, ROTATE_90, ROTATE_180, ROTATE_270 }
	public enum Connection { NOT_CONNECTED, CONNECTED }

	public const int none = 0;
	public const int top = 1;
	public const int right = 2;
	public const int bottom = 4; 
	public const int left = 8;

	public TileType type;
	public Angle angle;
	public Connection connectedState;

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

	public void isConnected () {
		if (connectedState == Connection.CONNECTED) {
			SpriteRenderer rend = GetComponent<SpriteRenderer>();
			rend.color = Color.yellow;
		}
	}
}