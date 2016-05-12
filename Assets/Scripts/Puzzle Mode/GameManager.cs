using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour {

	//pause menu 
	public GameObject pausePanel;

	//fade script
	private Fade fadeScript;

	//level manager script
	private LevelManager levelManagerScript;

	//game states
	public enum gameState {LOAD, TITLE, PLAY, SOLVED, PAUSE}
	public gameState state;
	public GameObject oneWay;
	public GameObject twoWay;
	public GameObject threeWay;
	public GameObject cross;

	private float oneWayPercent;
	private float twoWayPercent;
	private float threeWayPercent;
	private float crossPercent;

	//timer
	public Text timerText;
	float timer;

	//dimensions of board
	private int width;
	private int height;

	public GameObject[][] tiles;
	GameObject[][] prev;
	public Tile[][] tileScripts;

	public List<GameObject> connectedTiles;
	public List<GameObject> disconnectedTiles;
	//int index;

	//sliders and toggles
	public Slider soundSlider;
	public Slider musicSlider;
	public Toggle soundToggle;
	public Toggle musicToggle;

	//win text
	public Text win;

	//sounds and music
	public AudioSource[] sources;

	// Use this for initialization
	void Start () {
		width = 5;
		height = 5;
		//index = 0;

		//initialize 2d vectors
		tiles = new GameObject[width][];
		prev = new GameObject[width][];
		for (int i=0; i<width; i++) {
			tiles[i] = new GameObject[height];
			prev[i] = new GameObject[height];
		}

		connectedTiles = new List<GameObject>();
		disconnectedTiles = new List<GameObject>();
		levelManagerScript = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		timer = 15.0f;
		sources = GetComponents<AudioSource>();
		//change %s of tiles depending on the level
		if (levelManagerScript.level == 1) {
			oneWayPercent = 0.25f;
			twoWayPercent = 0.50f;
			threeWayPercent = 0.75f;
			crossPercent = 1.0f;
		}
		else if (levelManagerScript.level == 2) {
			oneWayPercent = 0.27f;
			twoWayPercent = 0.54f;
			threeWayPercent = 0.81f;
			crossPercent = 1.0f;
		}
		else if (levelManagerScript.level == 3) {
			oneWayPercent = 0.29f;
			twoWayPercent = 0.58f;
			threeWayPercent = 0.87f;
			crossPercent = 1.0f;
		}
		else if (levelManagerScript.level == 4) {
			oneWayPercent = 0.30f;
			twoWayPercent = 0.60f;
			threeWayPercent = 0.90f;
			crossPercent = 1.0f;
		}
		else {
			win.text = "YOU WIN";
		}
	
		fadeScript = GetComponent<Fade>();
		StartCoroutine(fadeScript.fadeIn());
		generateLevel ();
		state = gameState.LOAD;
	}

	// Update is called once per frame
	void Update () {
		if (state == gameState.LOAD) {
			if (fadeScript.fadingPanel.alpha == 0) {
				state = gameState.PLAY;
			}
		}
		if (state == gameState.PLAY) {
			//countdown timer, add 0s to both 1f if you want more digits
			timerText.text = "Time: " + Mathf.Round(timer * 1f) / 1f;
			timer -= Time.deltaTime;
			if (timer < 0) {
				levelManagerScript.solvedInTime = false;
				StartCoroutine (fadeScript.fadeOut());
				SceneManager.LoadScene("trumpEnemies");
				//Debug.Log ("done");
			}

			//refresh tile connection
			for (int i=0; i<5; i++) {
				for (int j=0; j<5; j++) {
					tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = false;
				}
			}
			//resetting gets rid of a bug, not sure what causes it, need more time to look into it
			check();
			check();
			check();
			check();
			if (Input.GetKeyDown(KeyCode.R)) {
				timer = 15.0f;
				//index = 0;
				clearTiles();
				generateLevel();
			}
		
			//if user presses ESC, pause the game
			if (Input.GetKeyDown (KeyCode.Escape)) {
				handlePause();
			}

			//win
			if (tiles[4][4].GetComponent<Tile>().connectedToTileConnectedToStart) {
				if (levelManagerScript.level < 4) {
					StartCoroutine (fadeScript.fadeOut());
					SceneManager.LoadScene("trumpEnemies");
				}
			}
		}
		//unpause if player presses ESC in pause state
		else if (state == gameState.PAUSE) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				handleUnpause();
			}
		}
		if (fadeScript.fadingPanel.alpha == 1) {
			SceneManager.LoadScene ("Menu");
		}
	}

	void check () {
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				//if tile is a one way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.ONE_WAY) {
					if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180) 
						&& (checkBottom (i, j) || checkTop (i, j)) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)
						&& (checkRight(i, j) || checkLeft(i, j)) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					else {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = false;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.white;
						if (i == 4 && j == 4) {
							tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
						}
					}
				}

				//if tile is a two way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.TWO_WAY) {
					//if rotated 0 degrees && check top or right
					if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 && (checkTop (i, j) || checkRight (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					//if rotated 90 degrees && check top or left
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 && (checkTop (i, j) || checkLeft (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					//if rotated 180 degrees && check left or bot
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 && (checkLeft (i, j) || checkBottom (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					//if rotated 270 degrees && check bot or right
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 && (checkBottom (i, j) || checkRight (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					else {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = false;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.white;
						if (i == 4 && j == 4) {
							tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
						}
					}
				}

				//if tile is a three way
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.THREE_WAY) {
					//if tile is rotated 0 degrees check top, right, bot
					if ((tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_0 && (checkTop (i, j) || checkRight (i, j) || checkBottom (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					//check right, top, left
					else if ((tiles[i][j].GetComponent<Tile>().angle  == Tile.Angle.ROTATE_90 && (checkRight (i, j) || checkTop (i, j) || checkLeft (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					//check bot, left, top
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 && (checkBottom (i, j) || checkLeft (i, j) || checkTop (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					else if ((tiles[i][j].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 && (checkLeft (i, j) || checkBottom (i, j) || checkRight (i, j))) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					else {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = false;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.white;
						if (i == 4 && j == 4) {
							tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
						}
					}
				}

				//if tile is a cross road
				if (tiles[i][j].GetComponent<Tile>().type == Tile.TileType.CROSS) {
					if ((checkTop (i, j) || checkRight (i, j) || checkBottom (i, j) || checkLeft (i, j)) || (i == 0 && j == 0)) {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.yellow;
					}
					else {
						tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = false;
						tiles[i][j].GetComponent<SpriteRenderer>().color = Color.white;
						if (i == 4 && j == 4) {
							tiles[i][j].GetComponent<SpriteRenderer>().color = Color.green;
						}
					}
				}
			}
		}
	}

	//check if top tile is connected
	bool checkTop (int x, int y) {
		return (y < 4 && tiles[x][y+1] && tiles[x][y+1].GetComponent<Tile>().connectedToTileConnectedToStart && 
			((tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
				(tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 || tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270 ||
					tiles[x][y+1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0)) || tiles[x][y+1].GetComponent<Tile>().type == Tile.TileType.CROSS)); //&& tiles[x][y+1].GetComponent<Tile>().connectedToTileConnectedToStart == true);
	}

	//check if bottom tile is connected
	bool checkBottom (int x, int y) {
		return (y > 0 && tiles[x][y-1] && tiles[x][y-1].GetComponent<Tile>().connectedToTileConnectedToStart && 
			((tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
				(tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90)) || 
				(tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
					tiles[x][y-1].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || tiles[x][y-1].GetComponent<Tile>().type == Tile.TileType.CROSS));// && tiles[x][y+1].GetComponent<Tile>().connectedToTileConnectedToStart == true);
	}

	//check if right tile is connected
	bool checkRight (int x, int y) {
		return (x < 4 && tiles[x+1][y] && tiles[x+1][y].GetComponent<Tile>().connectedToTileConnectedToStart && 
			((tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180)) || 
				(tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_180 ||
					tiles[x+1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[x+1][y].GetComponent<Tile>().type == Tile.TileType.CROSS)); //&& tiles[x][y+1].GetComponent<Tile>().connectedToTileConnectedToStart == true);
	}

	//check if left tile is connected
	bool checkLeft (int x, int y) {
		return (x > 0 && tiles[x-1][y] && tiles[x-1][y].GetComponent<Tile>().connectedToTileConnectedToStart  && 
			((tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.ONE_WAY && (tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 || tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.TWO_WAY && (tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || 
				(tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.THREE_WAY && (tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_0 || tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_90 ||
					tiles[x-1][y].GetComponent<Tile>().angle == Tile.Angle.ROTATE_270)) || tiles[x-1][y].GetComponent<Tile>().type == Tile.TileType.CROSS)); //&& tiles[x][y+1].GetComponent<Tile>().connectedToTileConnectedToStart == true);
	}

	//destroy all instantiated objects
	void clearTiles () {
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				Destroy (tiles[i][j]);
				Destroy (prev[i][j]);
				tiles[i][j] = null;
				prev[i][j] = null;
			}	
		}
		connectedTiles.Clear();
		disconnectedTiles.Clear();
	}

	//randomly place tiles 
	void generateLevel () {
		for (int i=0; i<5; i++){
			for (int j=0; j<5; j++){
				float randomGameObjectNum = Random.value;
				//for one-way tiles
				if (randomGameObjectNum < oneWayPercent) {
					int rotateAngle = Random.Range (1, 3); 
					if (rotateAngle == 1) {
						//GameObject tile = (GameObject)Instantiate (oneWay, new Vector3 (i, j, 0), Quaternion.Euler(0f, 0f, 90f));
						tiles[i][j] = (GameObject)Instantiate (oneWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 90f));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_90;
					}
					else {
						tiles[i][j] = ((GameObject)Instantiate (oneWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 0f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_0;
					}
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.ONE_WAY;
				}

				//for two-way tiles
				else if (randomGameObjectNum < twoWayPercent) {
					int rotateAngle = Random.Range (1, 5); 
					if (rotateAngle == 1) {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 90f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_90;
					}
					else if (rotateAngle == 2) {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 180f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_180;
					}
					else if (rotateAngle == 3) {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 270f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_270;
					}
					else {
						tiles[i][j] = ((GameObject)Instantiate (twoWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 0f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_0;
					}
					//start and end color change to yellow
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.TWO_WAY;
				}

				//for three-way tiles
				else if (randomGameObjectNum < threeWayPercent) {
					int rotateAngle = Random.Range (1, 5); 
					if (rotateAngle == 1) {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 90f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_90;
					}
					else if (rotateAngle == 2) {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 180f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_180;
					}
					else if (rotateAngle == 3) {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 270f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_270;
					}
					else {
						tiles[i][j] = ((GameObject)Instantiate (threeWay, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.Euler(0f, 0f, 0f)));
						tiles[i][j].GetComponent<Tile>().angle = Tile.Angle.ROTATE_0;
					}
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.THREE_WAY;
					//start and end color change to yellow
				}

				//for cross tiles
				else if (randomGameObjectNum < crossPercent){// (randomGameObjectNum == 4) {
					tiles[i][j] = ((GameObject)Instantiate (cross, new Vector3 (i * 0.9f, j * 0.9f, 0), Quaternion.identity));
					tiles[i][j].GetComponent<Tile>().type = Tile.TileType.CROSS;
					//start and end color change to yellow
				}
				//start and end color change to yellow
				if (i == 0 && j == 0) {
					tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = true;
				}
				if (i == 4 && j == 4) {
					tiles[i][j].GetComponent<Tile>().connectedToTileConnectedToStart = false;
					tiles[i][j].GetComponent<Tile>().endTile = true;
				}
			}
		}
	}
		
	public void handlePause () {
		pausePanel.GetComponent<CanvasGroup>().alpha = 1;
		pausePanel.GetComponent<CanvasGroup>().interactable = true;
		state = gameState.PAUSE;
	}

	public void handleUnpause () {
		pausePanel.GetComponent<CanvasGroup>().alpha = 0;
		pausePanel.GetComponent<CanvasGroup>().interactable = false;
		state = gameState.PLAY;
	}

	public void continueButton () {
		pausePanel.GetComponent<CanvasGroup>().alpha = 0;
		pausePanel.GetComponent<CanvasGroup>().interactable = false;
		//state = GameState.PLAY_ENDLESS;
		state =gameState.PLAY;
	}

	public void mainMenuButton () {
		StartCoroutine(fadeScript.fadeOut());
	}

	//SOUNDS AND MUSIC
	//sound slider
	public void updateSoundSlider () {
		if (soundToggle.isOn) {
			sources[1].volume = soundSlider.value;
		}
	}
	//sound toggle
	public void toggleSound () {
		if (!soundToggle.isOn) {
			sources[1].volume = 0f;
		}
		else {
			sources[1].volume = soundSlider.value;
		}
	}
	//music slider
	public void updateMusicSlider () {
		if (musicToggle.isOn) { 
			sources[0].volume = musicSlider.value;
		}
	}
	public void toggleMusic () {
		if (!musicToggle.isOn) {
			sources[0].volume = 0f;
		}
		else {
			sources[0].volume = musicSlider.value;
		}
	}
	public void exitButton () {
		//Application.Quit (); 		//exits game
		EditorApplication.isPlaying = false;	//stops scene in editor
	}
}

