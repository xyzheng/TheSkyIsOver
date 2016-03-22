using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;
	public float moveSpeed = 15f;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		rbody.MovePosition (rbody.position + movement_vector * moveSpeed * Time.deltaTime);
	}
}
