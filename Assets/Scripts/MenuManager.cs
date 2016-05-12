using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	//menu panels
	public GameObject menuPanel;
	public GameObject optionsPanel;

	private AudioSource bgm;
	private AudioSource hoverSound;

	public Toggle soundToggle;
	public Toggle musicToggle;
	public Slider soundSlider;
	public Slider musicSlider;

	//fading
	private Fade fadeScript;

	void Start () {
		fadeScript = GetComponent<Fade>();
		hoverSound = GetComponents<AudioSource>()[1];
		bgm = GetComponents<AudioSource>()[0];
	}

	public void playButton () {
		StartCoroutine (fadeScript.fadeOut());
		//StartCoroutine(fadeScript.fadeOut());
	}

	public void optionsButton () {
			//change options text back to size 30 before setting panel to false
			menuPanel.GetComponentsInChildren<Buttons>()[1].GetComponentInChildren<Text>().fontSize = 50;
			menuPanel.SetActive(false);
			optionsPanel.SetActive(true);
	}

	public void backButton () {
		//change back text back to size 30 before setting panel to false
		optionsPanel.GetComponentInChildren<Buttons>().GetComponentInChildren<Text>().fontSize = 50;
		menuPanel.SetActive(true);
		optionsPanel.SetActive(false);
	}

	//quit the game
	public void exitButton () {
		//Application.Quit (); 		//exits the exe
		EditorApplication.isPlaying = false;		//stops playing in the unity editor
	}

	void Update () {
		if (fadeScript.faded) {
			SceneManager.LoadScene ("Puzzle");
		}
	}

	//SOUNDS AND MUSIC
	//sound slider
	public void updateSoundSlider () {
		if (soundToggle.isOn) {
			hoverSound.volume = soundSlider.value;
		}
	}
	//sound toggle
	public void toggleSound () {
		if (!soundToggle.isOn) {
			hoverSound.volume = 0f;
		}
		else {
			hoverSound.volume = soundSlider.value;
		}
	}
	//music slider
	public void updateMusicSlider () {
		if (musicToggle.isOn) { 
			bgm.volume = musicSlider.value;
		}
	}
	public void toggleMusic () {
		if (!musicToggle.isOn) {
			bgm.volume = 0f;
		}
		else {
			bgm.volume = musicSlider.value;
		}
	}
}