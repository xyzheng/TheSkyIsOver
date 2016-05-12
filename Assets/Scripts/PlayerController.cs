using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public Text lose;
	public GameObject levelManager;
	public LevelManager lmScript;
	public GameObject gameManager;
	public GameObject playerBullet;
	public GameObject playerBullet2;
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	Rigidbody2D rbody;
	Animator anim;
	public float moveSpeed = 5f;
	public bool confused = false;
	public int health = 10;
	public float knockback;
	public float knockbackLength;
	public bool knockFromRight;
	public float knockbackCount;
	private float minX, maxX, minY, maxY;

	//scaling health bar
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager");
		lmScript = levelManager.GetComponent<LevelManager>();
		rbody = GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
		Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0,0, camDistance));
		Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1,1, camDistance));
		
		minX = bottomCorner.x;
		maxX = topCorner.x;
		minY = bottomCorner.y;
		maxY = topCorner.y;
		healthBar.transform.localScale = new Vector3(1f, 1f, 1f);

		if (lmScript.solvedInTime) {
			health = 10;
		}
		else {
			if (lmScript.hpLeft != 0) {
				health = lmScript.hpLeft;
			}
			else {
				health = 10;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (health);
		if (gameManager.GetComponent<GameManagement>().state == GameManagement.gameState.PLAY) {
			Vector2 pos = transform.position;

			// Horizontal contraint
			if (pos.x < minX) {
				pos.x = minX;
			}
			if (pos.x > maxX) {
				pos.x = maxX;
			}
			
			// vertical contraint
			if (pos.y < minY) {
				pos.y = minY;
			}
			if (pos.y > maxY) {
				pos.y = maxY;
			}
		
			transform.position = pos;

			if (Input.GetKeyDown (KeyCode.Space)) {
			
				GameObject bullet01 = (GameObject)Instantiate (playerBullet);
				bullet01.transform.position = bulletPosition1.transform.position;

				GameObject bullet02 = (GameObject)Instantiate(playerBullet2);
				bullet02.transform.position = bulletPosition2.transform.position;
			
			}
			if (knockbackCount <= 0) {
				if (!confused) {
					Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
					rbody.MovePosition (rbody.position + movement_vector * moveSpeed * Time.deltaTime);
				} else {
					Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Vertical"), Input.GetAxisRaw ("Horizontal"));
					rbody.MovePosition (rbody.position + movement_vector * moveSpeed * Time.deltaTime);
					StartCoroutine (ConfusionTime ());
				}
			} else {
				if (knockFromRight) {
					rbody.velocity = new Vector2 (-knockback, knockback);
				} else {
					rbody.velocity = new Vector2 (knockback, knockback);
				}
				knockbackCount -= Time.deltaTime;
			}

			if ((health <= 6) && (health > 3)) {
				anim.SetBool ("isHurt", true);
			} else if ((health <= 3) && (health > 0)) {
				anim.SetBool ("isVeryHurt", true);
			} else if (health <= 0) {
				lose.text = "You Lose";
				Destroy (gameObject);
				StartCoroutine (ConfusionTime());
				StartCoroutine (ConfusionTime());
				SceneManager.LoadScene ("Menu");
			}

			//health bar scale is according to the amount of health the player has
			healthBar.transform.localScale = new Vector3 (health * 0.1f, 1f, 1f);
		}
	}

	IEnumerator ConfusionTime(){
		yield return new WaitForSeconds (10);
		confused = false;
	}

	void OnTriggerEnter2D(Collider2D col) {
		HurtEffect hurtEffect = GetComponent<HurtEffect> ();
		hurtEffect.displayHurtEffect = true;

		if ((col.tag == "EnemyBullet") || (col.tag == "Enemy")) {
			health -= 1;
			lmScript.hpLeft = health;
		}

		if (col.tag == "BunnyBullet") {
			health -= 1;
			confused = true;
			lmScript.hpLeft = health;
		}

		if (col.tag == "UmbrellaBullet") {
			if (col.transform.position.x < transform.position.x){
				knockFromRight = false;
			} else {
				knockFromRight = true;
			}
			knockbackCount = knockbackLength;
		}
	}
}
