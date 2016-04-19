using UnityEngine;
using System.Collections;

public class MoveEnemyBullet : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("catwithoutarms");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0.1f, 0f, 0f);//(transform.position - player.transform.position).normalized * Time.deltaTime;
	}
}
