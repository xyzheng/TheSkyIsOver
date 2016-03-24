using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public enum ConnectionType { FAILED, CONNECTED, CONNECTED_RIGHT }

	public int x;
	public int y;

	//constructor
	public Tile (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public const int top = 1;
	public const int right = 2;
	public const int bottom = 4;
	public const int left = 8;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool isConnected () {

	}
}
