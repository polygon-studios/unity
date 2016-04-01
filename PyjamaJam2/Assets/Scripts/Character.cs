using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	
	public Items gameMaster;
	public SocketIOLogic io;
	
    // Controller variables
	public KeyCode inputLeft;
	public KeyCode inputRight;
	public KeyCode inputJump;
	public KeyCode inputItem;
    public KeyCode lastPressedKey;

    public string charID;
	public int controllerNumber;
	string playerName = "Player";
	string controllerXAxis = "_JoystickX";
	string controllerA = "_A";
	string controllerX = "_X";

    // Movement variables
    public float currentJump = 0f;
    public float starterJump;
    public bool invincible;
    public float currentSpeed; //walk 0.5f //run 0.8f
    public float starterSpeed;
    public bool noJump = false;

    float lastY;
    float moveSpeed = 0f;

    public bool onGround = false;
    public void isOnGround(bool passThrough) {  onGround = passThrough; }

	//VFX variables
	public GameObject jumpVFX;
	public GameObject landedVFX;

    // Item variables
    public GameObject latern;
	public GameObject GM;
	float itemDebounceTimerSaveTime = 0.03f; //seconds
    float itemDebounceTimer;
	Latern laternScript;
	GameMaster GMScript;
    public bool isStunned;
    float fishTimer = 10;
    bool isFished;

	List<GameObject> jailDoors = new List<GameObject>();
    
    bool isDark;

    //audio
    public AudioClip audioEffectJump;
    public AudioClip audioEffectJumpWithBunnyPowers;
    public AudioClip audioEffectLand;
    public AudioClip audioEffectFire;
    public AudioClip audioEffectItemPickup;
    public AudioClip audioEffectStunnedHit;
    public AudioClip audioEffectDoorClosed;
    public AudioClip audioEffectPineconeHit;

	public Animator animator;
	Rigidbody2D rigidbody;
	Renderer rend;
	Item item; //holds item

	
	Vector3 lastPosition = Vector3.zero;


	// Use this for initialization
	void Start () {

       
		animator = this.gameObject.GetComponent<Animator> ();
		rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer>();
		currentJump = starterJump;
        currentSpeed = starterSpeed;
		itemDebounceTimer = itemDebounceTimerSaveTime; 

		laternScript = latern.GetComponent<Latern> ();
		GMScript = GM.GetComponent<GameMaster> ();

		invincible = false;

		var objects = GameObject.FindGameObjectsWithTag("jailDoor");
		foreach (GameObject obj in objects) {
			jailDoors.Add(obj);
			obj.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Physics2D.IgnoreLayerCollision(8,8, true);
		Physics2D.IgnoreLayerCollision(8,9, true);

		lastY = transform.position.y;
		Movement ();
		ItemCheck ();
		checkCollisions ();

		if (laternScript.isActivated == true) {
			updateLatern();
		}
        if (isFished)
            updateFish();

	}

	void Movement(){
        /*
		A button used to jump
		X button used for picking up, activating, and using items
		B button used to drop items
		*/


        moveSpeed = Mathf.Abs((transform.position.x - lastPosition.x));
		lastPosition = transform.position;
		animator.SetFloat ("moveSpeed", moveSpeed*50);

		if (Input.GetKey (inputRight) && isStunned == false) { //moving character right
			transform.Translate (currentSpeed * Time.deltaTime, 0.0f, 0.0f);
			transform.eulerAngles = new Vector2 (0, 0);
			lastPressedKey = inputRight;

		}
		
		if (Input.GetKey (inputLeft) && isStunned == false) {//move character left
			transform.Translate(currentSpeed * Time.deltaTime, 0.0f, 0.0f);
			transform.eulerAngles = new Vector2(0, 180);
			lastPressedKey = inputLeft;
		}
		if(Input.GetAxis(playerName + controllerNumber + controllerXAxis) != 0 && isStunned == false){
			transform.Translate(Vector3.right*Mathf.Abs(Input.GetAxis(playerName + controllerNumber + controllerXAxis))* currentSpeed * Time.deltaTime);
			if (Input.GetAxis(playerName + controllerNumber + controllerXAxis) < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
                lastPressedKey = inputLeft;
            }
            else {
                transform.eulerAngles = new Vector2(0, 0);
                lastPressedKey = inputRight;
            }
		}


		//if (Input.GetKey (inputJump) && onGround) {
		if((Input.GetButtonDown(playerName + controllerNumber + controllerA) || Input.GetKey (inputJump)) && onGround && isStunned == false && noJump == false){ //
			onGround = false;
			GetComponent<Rigidbody2D>().AddForce(transform.up * currentJump);
			animator.SetTrigger ("jump");

			//jump VFX
			if (jumpVFX != null) {
				GameObject vfx = (GameObject)Instantiate (jumpVFX, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.identity);
			}

            if (currentJump > starterJump )
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.PlayOneShot(audioEffectJumpWithBunnyPowers, 0.2f);
            }
            else{
                AudioSource audio = GetComponent<AudioSource>();
                audio.PlayOneShot(audioEffectJump, 0.2f);
            }
		}

        //code for when char lands, but repeats every time onGround is true...fix this
        bool canPlay;
        canPlay = true;
        if (onGround){
            AudioSource audio = GetComponent<AudioSource>();
            if (!audio.isPlaying && canPlay == true){
                canPlay = false;
                //audio.PlayOneShot(audioEffectLand, 0.2f);
            }
        }
       
        
	}

	public void activateLight(){
		if (latern != null)
			laternScript.activateLatern ();
        isDark = true;
        animator.SetBool("night", true);
	}

	void updateLatern(){
		laternScript.updateWithCharData (transform.position.x, transform.position.y);
        if(laternScript.checkLightBrightness() == false)
        {
            goHome();
            laternScript.resetLight(transform.position.x, transform.position.y);
        }
	}

    void goHome()
    {
        if (transform.position.x > 14.5)
            transform.position = new Vector3(17.3f, 1.5f, transform.position.z);
        else
            transform.position = new Vector3(12.3f, 1.5f, transform.position.z);
    }

	void ItemCheck(){
		if (item != null) {
			Debug.Log (playerName + controllerNumber + controllerX);
			if(item.beenTriggered == true){
				item.doUpdate();
			}
			else if(Input.GetKey(inputItem) || Input.GetButtonDown(playerName + controllerNumber + controllerX)){
				itemDebounceTimer -= Time.deltaTime;
				
				if (itemDebounceTimer < 0f) {

					item.TriggerEffect();
					item.beenTriggered = true;
					animator.SetBool ("bindle", false);
				}
			}else{
				animator.SetBool ("bindle", true);
			}
			
		}
        else
        {
            animator.SetBool("bindle", false);
        }

	}

	void checkCollisions(){
        List<GameObject> items = new List<GameObject>();
        items.AddRange(gameMaster.lev1ItemsCurrent);
        items.AddRange(gameMaster.lev2ItemsCurrent);
        items.AddRange(gameMaster.lev3ItemsCurrent);
        items.AddRange(gameMaster.currentOil);


        if (items != null){
			foreach(GameObject eItem in items)
			{
				float itemX = eItem.transform.position.x;
				float itemY = eItem.transform.position.y;
				if(itemX + 0.4 > transform.position.x && itemX - 0.4 < transform.position.x && itemY + 0.5 > transform.position.y && itemY - 0.5 < transform.position.y){
					//Debug.Log (eItem.name + " item near character: " + this.name);
					doCollision (eItem);
				}

			}
		}

				   

	}

    //this is where I think the glitch lies that one item can be picked up by more than one character
    //the audio is left in so you can hear the repeating initiations of the item pickup sound
    //when debugged, the first 'if' statement triggers 4-16 messages(ie 4-16 collision and button press itteration frames)
    //depending on the item...before the items is destroyed. Hope this helps. Could be wrong, because I am 
    //pretty noob at coding, but it seems like it could be it. |

	void doCollision(GameObject objectHit){
		if(Input.GetKey(inputItem) ||Input.GetButtonDown(playerName + controllerNumber + controllerX)){
               
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
				}
                else if (objectHit.gameObject.GetComponent<Firework>() != null)
                {
                    Firework firework = objectHit.gameObject.GetComponent<Firework>();
                    firework.initVariables(this.gameObject.GetComponent<Character>());
                    firework.Hide();

                    item = firework;
                }/*else{
					Item newItem = objectHit.gameObject.GetComponent<Item>();
					newItem.Hide();
					item = newItem;
				}*/
            AudioSource audio = GetComponent<AudioSource>();
                audio.PlayOneShot(audioEffectItemPickup, 0.2f);
			}
	}

	void OnTriggerEnter2D(Collider2D objectHit){
		if (objectHit.gameObject.tag == "coin") {
			//increase points
			
			Coin coin = objectHit.gameObject.GetComponent<Coin>();
			GMScript.addPoints(this.name, coin.points);
			coin.destroySelf();
			
		}

        
		if (objectHit.gameObject.tag == "Trap" && invincible == false) {
			if(objectHit.gameObject.name.Contains ("button")){
				foreach (var obj in jailDoors) {
					obj.SetActive(true);
				}
				io.lockHouse();
			} else {
				stunCharacter(3, false);
			}
			if(objectHit.gameObject.name.Contains ("bramble")){

			}
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(audioEffectStunnedHit, 0.2f );
            audio.PlayOneShot(audioEffectPineconeHit, 0.2f);


			Traps trap = objectHit.gameObject.GetComponent<Traps> ();
			trap.destroySelf ();
		}
        
		if (objectHit.gameObject.tag == "Enemy" && invincible == false) {
			stunCharacter(3, false);
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(audioEffectStunnedHit, 0.2f);
		}

		if (objectHit.gameObject.tag == "Projectile" && invincible == false) {
			stunCharacter(3, false);
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(audioEffectStunnedHit, 0.2f);

			Rock rock = objectHit.gameObject.GetComponent<Rock>();
			rock.destroySelf();
		}

		if (objectHit.gameObject.tag == "mushroom" && invincible == false) {
			if(transform.position.y > objectHit.gameObject.transform.position.y){
				Vector2 force = new Vector2 (0, 8);
				Rigidbody2D rb = GetComponent<Rigidbody2D>();
				rb.AddForce(force, ForceMode2D.Impulse);
			}

		}


		if (objectHit.gameObject.tag == "Door") {
			string itemHeld;
			string side;
			bool hasItem = false;

			if(item){
                itemHeld = item.name;
				hasItem = true;
			}
			else {
				itemHeld = "none";
			}

			if(objectHit.gameObject.name.Contains("Right")){
				side = "right";
			}
			else {
				side = "left";
			}
			stunCharacter(2, true, side, hasItem);
			io.playerEnter(this.name, side, itemHeld);
            if (hasItem)
            {
                GMScript.addPoints(this.name, item.points);
				laternScript.resetLight(transform.position.x, transform.position.y);
                item = null;
            }

		}
		if (objectHit.gameObject.tag == "Ground") {
			if (transform.position.y < 0.453f) {
				onGround = true;
			}
		}


	}

	public bool isFalling(){

		return (lastY > transform.position.y);
	}

	public void stunCharacter(int duration, bool houseMove = false, string side = "right", bool hasItem = false){
		isStunned = true;
        animator.SetBool("stun", true);

        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioEffectDoorClosed);

        if (houseMove) {
			StartCoroutine (Unstun (duration, true, side, hasItem));
		} else {
			StartCoroutine (Unstun (duration));
		}
	}

	IEnumerator Unstun(int stunDuration, bool houseMove = false, string side = "right", bool hasItem = false)
	{
		if (houseMove) {
			Vector3 houseCenter = new Vector3 (14.3f, 1.82f, -6.0f);
			transform.position = houseCenter;

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //object1.rigidbody.velocity = Vector3.zero;
        }

		int seconds = stunDuration;

		if(hasItem)
			seconds += 7;

		// wait 3 seconds and continue
		yield return new WaitForSeconds(seconds);

		if (houseMove) {
			Vector3 temp;
			if(side.Contains("left")){
				temp = new Vector3(16.33f,0.54f,-6.0f);
			}
			else {
				temp = new Vector3(12.8f,0.54f,-6.0f);
			}
			transform.position = temp;
		}
		
		isStunned = false;
        animator.SetBool("stun", false);
	}

    public void isFishedTrigger()
    {
        if (isFished == true)
            fishTimer += 10;
        
        animator.SetBool("fish", true);
        isStunned = true;
        isFished = true;

    }

    void updateFish()
    {
        fishTimer -= Time.deltaTime;

        if (fishTimer < 0)
        {
            isFished = false;
            isStunned = false;
            animator.SetBool("fish", false);
            fishTimer = 10;
        }
    }

	public void justLanded(){
		GameObject vfx = (GameObject)Instantiate (landedVFX, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.identity);
	}

	public void destroySelf(){
		Destroy (this.gameObject);
	}

}
