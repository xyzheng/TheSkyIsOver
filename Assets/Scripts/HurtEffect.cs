using UnityEngine;
using System.Collections;

public class HurtEffect : MonoBehaviour {

	public Texture hurtEffect;
	public float displayTime = .5f;
	public bool displayHurtEffect = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (displayHurtEffect == false) {

		}
	}

	void OnGUI(){
		if (displayHurtEffect == true) {
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), hurtEffect, ScaleMode.StretchToFill);
			StartCoroutine (StopDisplayingEffect());
		}
	}

	IEnumerator StopDisplayingEffect(){
		yield return new WaitForSeconds (displayTime);
		displayHurtEffect = false;
	}
}
