using UnityEngine;
using System.Collections;

public class Slippers : Item{

	int effectTimer; 
	Character character;
	float timer = 20; //in seconds
	float newHeightJump = 500f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (this.character != null) {
			updateTrigger ();
		}
	}
	public void TriggerEffect(Character currCharacter){
		this.character = currCharacter;
		this.character.currentJump = newHeightJump;
		timer -= Time.deltaTime;

	}

	void updateTrigger(){
		timer -= Time.deltaTime;

   		if (timer < 0) {
			Reset();
			base.DestroySelf();
			Destroy(this.gameObject);
		}
	}

	void Reset(){
		//jump sequence ends
		character.currentJump = character.starterJump;
	}
}
