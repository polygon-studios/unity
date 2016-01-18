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
			if(item.beenTriggered == true)
				item.doUpdate();
			else if(Input.GetKeyDown(KeyCode.I)){
			    item.TriggerEffect();
				item.beenTriggered = true;
			}

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

		///picking up an item
		if(objectHit.gameObject.tag == "item"){

			if(objectHit.gameObject.GetComponent<Chili>() != null){
				//requires other multiplayer prior to coding

				/*Chili chili = objectHit.gameObject.GetComponent<Chili>();
				chili.TriggerEffect(characters[], this.gameObject.GetComponent<Character>()); //requires list of all other characters, and the current one
				chili.Hide();
				
				item = chili;*/
			}else if(objectHit.gameObject.GetComponent<Pinwheel>() != null){
				Pinwheel pinwheel = objectHit.gameObject.GetComponent<Pinwheel>();
				pinwheel.initVariables(this.gameObject.GetComponent<Character>());
				pinwheel.Hide();

				item = pinwheel;
				//item.TriggerEffect();

			}else if(objectHit.gameObject.GetComponent<Slippers>() != null){
				Slippers slippers = objectHit.gameObject.GetComponent<Slippers>();
				slippers.initVariables(this.gameObject.GetComponent<Character>());
				slippers.Hide();
				
				item = slippers;
				//item.TriggerEffect();
			}else if(objectHit.gameObject.GetComponent<TreasureChest>() != null){
				TreasureChest treasure = objectHit.gameObject.GetComponent<TreasureChest>();
				treasure.initVariables(this.gameObject.GetComponent<Character>());
				treasure.Hide();
				
				item = treasure;
				//item.TriggerEffect();
			}


		}
	}

}
