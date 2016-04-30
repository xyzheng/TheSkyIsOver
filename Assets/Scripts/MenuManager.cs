using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	//menu panels
	public GameObject menuPanel;
	public GameObject optionsPanel;

	//fading effect objects
	public CanvasGroup fadingPanel;
	private float fadeSpeed;
	public bool isFading;
	private bool faded;

	public void Start () {
		fadingPanel = GameObject.Find ("Canvas").GetComponentInChildren<CanvasGroup>();
		fadeSpeed = 1.0f;
		isFading = false;
		faded = false;
	}

	public void playButton () {
		if (!isFading) {
			StartCoroutine(fadeOut());
		}
		//StartCoroutine(fadeScript.fadeOut());
	}

	public void optionsButton () {
		if (!isFading) {
			//change options text back to size 30 before setting panel to false
			menuPanel.GetComponentsInChildren<Buttons>()[1].GetComponentInChildren<Text>().fontSize = 30;
			menuPanel.SetActive(false);
			optionsPanel.SetActive(true);
		}
	}

	public void backButton () {
		//change back text back to size 30 before setting panel to false
		optionsPanel.GetComponentInChildren<Buttons>().GetComponentInChildren<Text>().fontSize = 30;
		menuPanel.SetActive(true);
		optionsPanel.SetActive(false);
	}

	//quit the game
	public void exitButton () {
		//Application.Quit (); 		//exits the exe
		EditorApplication.isPlaying = false;		//stops playing in the unity editor
	}

	void Update () {
		if (faded) {
			SceneManager.LoadScene ("Puzzle");
		}
		//if (fadeScript.fadingPanel.alpha == 0) {
		//	SceneManager.LoadScene ("Puzzle");
		//}
	}

	//fade to black effect
	public IEnumerator fadeOut () {
		isFading = true;
		float fadeTime = 1.0f;
		while (fadingPanel.alpha < 1) {
			fadingPanel.alpha += Time.deltaTime / fadeTime;
			yield return null;
		}
		faded = true;
		isFading = false;
	}

}