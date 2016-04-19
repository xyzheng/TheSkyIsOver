using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public GameObject gm;
	GameManager gmScript;

	// Use this for initialization
	void Start () {
		gmScript = gm.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (gmScript.state);
		if (gmScript.state == GameManager.gameState.SOLVED) {
			StartCoroutine (doFade ());
		}
	}

	public void fade () {

	}

	IEnumerator doFade () {
		CanvasGroup cg = GetComponent<CanvasGroup>();
	
			while (cg.alpha > 0) {
				cg.alpha -= Time.deltaTime / 2;
				yield return null;
			}

		cg.interactable = false;
		yield return null;
	}
}
