using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public bool onGround = false;

	public void isOnGround(bool passThrough){
		onGround = passThrough;
	}
	public float currentJump = 0f;
	public float starterJump;
	public KeyCode inputLeft;
	public KeyCode inputRight;
	public KeyCode inputJump;
	public string charID;
	public KeyCode lastPressedKey;
	public string controllerXAxis;
	public string controllerA;
	public string controllerX;

	public GameObject latern;


	float itemDebounceTimerSaveTime = 0.03f; //seconds
	float itemDebounceTimer; 
	bool itemDebounceTimeDone = false;
	float lastY;
	float moveSpeed = 0f;
	Latern laternScript;
	bool isStunned;


	Animator animator;
	Rigidbody2D rigidbody;
	Item item; //holds item

	public float speed; //walk 0.5f //run 0.8f
	Vector3 lastPosition = Vector3.zero;



	// Use this for initialization
	void Start () {
		animator = this.gameObject.GetComponent<Animator> ();
		rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
		currentJump = starterJump;
		itemDebounceTimer = itemDebounceTimerSaveTime; 

		laternScript = latern.GetComponent<Latern> ();
	}
	
	// Update is called once per frame
	void Update () {
		lastY = transform.position.y;
		Movement ();
		ItemCheck ();

		if (laternScript.isActivated == true) {
			updateLatern();
		}
	}

	void Movement(){
		/*
		A button used to jump
		X button used for picking up, activating, and using items
		B button used to drop items
		*/

		moveSpeed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
		animator.SetFloat (charID +"MoveSpeed", moveSpeed*50);

		if (Input.GetKey (inputRight) && isStunned == false) { //moving character right
			transform.Translate (speed * Time.deltaTime, 0.0f, 0.0f);
			transform.eulerAngles = new Vector2 (0, 0);
			lastPressedKey = inputRight;
		}
		
		if (Input.GetKey (inputLeft) && isStunned == false) {//move character left
			transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
			transform.eulerAngles = new Vector2(0, 180);
			lastPressedKey = inputLeft;
		}
		if(Input.GetAxis(controllerXAxis) != 0 && isStunned == false){
			transform.Translate(Vector3.right*Mathf.Abs(Input.GetAxis(controllerXAxis))*speed*Time.deltaTime);
			if(Input.GetAxis(controllerXAxis) < 0)
				transform.eulerAngles = new Vector2(0, 180);
			else
				transform.eulerAngles = new Vector2 (0,0);
		}

		//if (Input.GetKey (inputJump) && onGround) {
		if((Input.GetButtonDown(controllerA) || Input.GetKey (inputJump)) && onGround && isStunned == false){ //
			onGround = false;
			GetComponent<Rigidbody2D>().AddForce(transform.up * currentJump);
			animator.SetTrigger (charID +"Jump");
		}
	}

	public void activateLight(){
		if (latern != null)
			laternScript.activateLatern ();
	}

	void updateLatern(){
		laternScript.updateWithCharData (transform.position.x, transform.position.y);
	}

	void ItemCheck(){
		if (item != null) {

			if(item.beenTriggered == true){
				item.doUpdate();
			}
			else if(Input.GetKey(KeyCode.I) || Input.GetButtonDown(controllerX)){
				itemDebounceTimer -= Time.deltaTime;
				
				if (itemDebounceTimer < 0f) {

					item.TriggerEffect();
					item.beenTriggered = true;
					animator.ResetTrigger (charID + "Bindle");
				}
			}else{
				animator.SetTrigger (charID + "Bindle");
			}
			
		}

	}

	void OnTriggerStay2D(Collider2D objectHit){

		///picking up an item
		if (objectHit.gameObject.tag == "item" && item == null ) {
			if(Input.GetKey(KeyCode.I) ||Input.GetButtonDown(controllerX)){
				itemDebounceTimer = itemDebounceTimerSaveTime;

				if (objectHit.gameObject.GetComponent<Chili> () != null) {
					//requires other multiplayer prior to coding

					Chili chili = objectHit.gameObject.GetComponent<Chili>();
					chili.initVariables (this.gameObject.GetComponent<Character> ());
					//chili.TriggerEffect(characters[], this.gameObject.GetComponent<Character>()); //requires list of all other characters, and the current one
					chili.Hide();
					
					item = chili;
				}else if (objectHit.gameObject.GetComponent<Fish> () != null) {
					Fish fish = objectHit.gameObject.GetComponent<Fish> ();
					fish.initVariables (this.gameObject.GetComponent<Character> ());
					fish.Hide ();
					
					item = fish;
				}else if (objectHit.gameObject.GetComponent<GhostItem> () != null) {
					GhostItem ghost = objectHit.gameObject.GetComponent<GhostItem> ();
					ghost.initVariables (this.gameObject.GetComponent<Character> ());
					ghost.Hide ();
					
					item = ghost;
				} else if (objectHit.gameObject.GetComponent<OilLatern> () != null) {
					OilLatern oil = objectHit.gameObject.GetComponent<OilLatern> ();
					oil.initVariables (this.gameObject.GetComponent<Character> ());
					oil.Hide ();
					
					item = oil;
				}else if (objectHit.gameObject.GetComponent<Pinwheel> () != null) {
					Pinwheel pinwheel = objectHit.gameObject.GetComponent<Pinwheel> ();
					pinwheel.initVariables (this.gameObject.GetComponent<Character> ());
					pinwheel.Hide ();

					item = pinwheel;
				} else if (objectHit.gameObject.GetComponent<Prune> () != null) {
					//requires other multiplayer prior to coding
					Prune prune = objectHit.gameObject.GetComponent<Prune>();
					prune.initVariables (this.gameObject.GetComponent<Character> ());
					prune.Hide();
					
					item = prune;
				} else if (objectHit.gameObject.GetComponent<Slippers> () != null) {
					Slippers slippers = objectHit.gameObject.GetComponent<Slippers> ();
					slippers.initVariables (this.gameObject.GetComponent<Character> ());
					slippers.Hide ();
					
					item = slippers;
				} else if (objectHit.gameObject.GetComponent<TreasureChest> () != null) {
					TreasureChest treasure = objectHit.gameObject.GetComponent<TreasureChest> ();
					treasure.initVariables (this.gameObject.GetComponent<Character> ());
					treasure.Hide ();
					
					item = treasure;
				}/*else{
					Item newItem = objectHit.gameObject.GetComponent<Item>();
					newItem.Hide();
					item = newItem;
				}*/
			}

		} else if (objectHit.gameObject.tag == "coin") {
			//increase points

			Coin coin = objectHit.gameObject.GetComponent<Coin>();
			coin.destroySelf();

		}

		if (objectHit.gameObject.tag == "Trap") {
			stunCharacter();
		}
	}

	public bool isFalling(){

		return (lastY > transform.position.y);
	}

	public void stunCharacter(){
		isStunned = true;
		StartCoroutine(Unstun(1.0f));
	}

	IEnumerator Unstun(float disValue)
	{
		// wait 3 seconds and continue
		yield return new WaitForSeconds(3);
		
		isStunned = false;
	}

}
