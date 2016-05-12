using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {

	public int enemiesLeft = 10;

	public enum gameState { LOAD, PLAY, PAUSE }
	public gameState state;
	public GameObject pausePanel;

	private Fade fadeScript;

	// Use this for initialization
	void Start () {
		fadeScript = GetComponent<Fade>();
		StartCoroutine(fadeScript.fadeIn());
		state = gameState.LOAD;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemiesLeft == 0) {
			StartCoroutine(fadeScript.fadeOut());
			SceneManager.LoadScene ("Puzzle");
			//Application.LoadLevel (Random.Range (0, 2));
		}

		if (state == gameState.LOAD) {
			if (fadeScript.fadingPanel.alpha == 0) {
				state = gameState.PLAY;
			}
		}
		else if (state == gameState.PLAY) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				handlePause ();
			}
		}
		else if (state == gameState.PAUSE) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				handleUnpause ();
			}
		}
		if (fadeScript.fadingPanel.alpha == 1) {
			SceneManager.LoadScene ("MainMenu");
		}
	}

	public void handlePause () {
		pausePanel.GetComponent<CanvasGroup>().alpha = 1;
		state = gameState.PAUSE;
	}

	public void handleUnpause () {
		pausePanel.GetComponent<CanvasGroup>().alpha = 0;
		state = gameState.PLAY;
	}

	public void continueButton () {
		pausePanel.SetActive(false);
		//state = GameState.PLAY_ENDLESS;
		state =gameState.PLAY;
	}

	public void mainMenuButton () {
		StartCoroutine(fadeScript.fadeOut());
	}
}
