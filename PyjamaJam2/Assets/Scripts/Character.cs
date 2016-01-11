using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public bool onGround = false;
	public void isOnGround(bool passThrough){
		onGround = passThrough;
	}
	public float currentJump = 0f;
	public float starterJump = 300f;

	Animator animator;
	Item item; //holds item

	float speed = 0.8f; //walk 0.5f //run 0.8f


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		currentJump = starterJump;
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		if (item != null) {
			item.doUpdate();
		}
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
			GetComponent<Rigidbody2D>().AddForce(transform.up * currentJump);
			animator.SetTrigger ("foxJump");
		}
	}

	void OnTriggerEnter2D(Collider2D objectHit){
		if(objectHit.gameObject.tag == "item"){
			Slippers slippers = objectHit.gameObject.GetComponent<Slippers>();
			slippers.TriggerEffect(this.gameObject.GetComponent<Character>());
			slippers.Hide();

			item = slippers; 
		}
	}

}
