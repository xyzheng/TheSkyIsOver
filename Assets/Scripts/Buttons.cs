using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	Text buttonText;
	AudioSource hoverSound;
	bool isPlaying;
	AudioSource[] clips;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
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
		buttonText.fontSize = 35;
		//hoverSound.Play();
		isPlaying = true;
	}

	public void OnPointerExit (PointerEventData e) {
		buttonText.fontSize = 30;	
		/*
		if (isPlaying && hoverSound != null) {
			hoverSound.Stop();
		}
		*/
	}
}