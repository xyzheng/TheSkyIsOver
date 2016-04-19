using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShooterGameManager : MonoBehaviour {

	public enum gameState {LOAD, TITLE, PLAY, DONE}
	public gameState state;

	public GameObject player;
	public PlayerController playerScript;
	public Text HPText;

	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<PlayerController>();
		state = gameState.PLAY;
	}
	
	// Update is called once per frame
	void Update () {
		HPText.text = "HP: " + playerScript.playerHP;
	}


}
