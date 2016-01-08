using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public bool onGround = false;
	public void isOnGround(bool passThrough){
		onGround = passThrough;
	}

	float speed = 0.8f; //walk 0.5f //run 
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
	
	}

	void Movement(){
		animator.SetFloat ("foxMoveSpeed", Mathf.Abs (Input.GetAxis ("Horizontal")));

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			transform.Translate(Vector3.right*speed*Time.deltaTime);
			transform.eulerAngles = new Vector2 (0,0);
		}
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 180);
		}
		if (Input.GetKeyDown (KeyCode.Space) && onGround) {
			onGround = false;
			GetComponent<Rigidbody2D>().AddForce(transform.up * 300f);
			animator.SetTrigger ("foxJump");
		}
	}	
}
