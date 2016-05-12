using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	Text buttonText;
	bool isPlaying;

	AudioSource hoverSound;
	AudioSource[] clips;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
		clips = GameObject.Find("Menu Manager").GetComponents<AudioSource>();
		hoverSound = clips[1];
		/*
		if (SceneManager.GetActiveScene().name == "Menu") {
			hoverSound = GameObject.Find("Menu Manager").GetComponent<AudioSource>();
		}
		else {//if (SceneManager.GetActiveScene().name == "Puzzle"){
			clips = GameObject.Find("GameManager").GetComponents<AudioSource>();
			hoverSound = clips[4];
		}
		*/
		isPlaying = false;
	}

	public void OnPointerEnter (PointerEventData e) {
		buttonText.fontSize = 55;
		hoverSound.Play();
		isPlaying = true;
	}

	public void OnPointerExit (PointerEventData e) {
		buttonText.fontSize = 50;	
		if (isPlaying && hoverSound != null) {
			hoverSound.Stop();
		}
	}
}