using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	public float moveSpeed = 1f;
	public float rotationSpeed = 1f;
	GameManagement gameManagement;
	
	void Start() 
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		gameManagement = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();
	}
	
	void Update() 
	{    
		if (gameManagement.state == GameManagement.gameState.PLAY) {
			if (target != null) {
				Vector2 dir = target.position - transform.position;
				if (dir != Vector2.zero) 
					transform.rotation = Quaternion.Slerp (transform.rotation, 
					                                       Quaternion.FromToRotation (Vector2.right, dir), 
				    	                                   rotationSpeed * Time.deltaTime);
			
				//Move Towards Target
				transform.position += (target.position - transform.position).normalized 
					* moveSpeed * Time.deltaTime;
			}	 
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"){
			Destroy (gameObject);
			gameManagement.enemiesLeft -= 1;
		}
	}

}
