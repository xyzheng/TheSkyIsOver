using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerHP;
	public bool immune;
	bool flickering;

	public GameObject playerBullet;
	public GameObject playerBullet2;
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	Transform leftArm;
	Transform rightArm;
	Rigidbody2D rbody;
	Animator anim;
	public float moveSpeed = 15f;

	//sounds
	AudioSource laser;

	// Use this for initialization
	void Start () {
		laser = GameObject.Find("GameManager").GetComponent<AudioSource>();
		rbody = GetComponent<Rigidbody2D> ();
		playerHP = 5;
		immune = false;
		flickering = false;
		leftArm = transform.GetChild(2);
		rightArm = transform.GetChild(3);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			laser.Play();
			GameObject bullet01 = (GameObject)Instantiate (playerBullet);
			bullet01.transform.position = bulletPosition1.transform.position;

			GameObject bullet02 = (GameObject)Instantiate(playerBullet2);
			bullet02.transform.position = bulletPosition2.transform.position;
		
		}

		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		rbody.MovePosition (rbody.position + movement_vector * moveSpeed * Time.deltaTime);

		//if player has 0 hp left, die
		if (playerHP == 0) {
			Destroy (transform.gameObject);
		}
	}

	//getting hit by enemy shots damages player by 1 
	void OnTriggerEnter2D(Collider2D col){
	
		if ((col.tag == "EnemyBullet") || (col.tag == "Enemy")) {
			StartCoroutine(flicker());
			playerHP --;
			Destroy (col.gameObject);
		}
	}

	//flicker when player gets hit
	public IEnumerator flicker () {
		flickering = true;
		float duration = 1.0f;
		//Debug.Log (duration);
		while (duration > 0f) {
			duration -= Time.deltaTime;
			Debug.Log (Time.deltaTime);
			transform.GetComponent<SpriteRenderer>().enabled = !transform.GetComponent<SpriteRenderer>().enabled;
			leftArm.GetComponent<SpriteRenderer>().enabled = !leftArm.GetComponent<SpriteRenderer>().enabled;
			rightArm.GetComponent<SpriteRenderer>().enabled = !rightArm.GetComponent<SpriteRenderer>().enabled;
			yield return new WaitForSeconds (0.5f);
		}
		transform.GetComponent<SpriteRenderer>().enabled = true;
		//Debug.Log (duration);
	}

}
