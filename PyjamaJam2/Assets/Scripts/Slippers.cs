using UnityEngine;
using System.Collections;

public class Slippers : Item{

	int effectTimer; 
	Character character;
	float timer = 10; //in seconds
	float newHeightJump = 500f;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		//base.Update ();
 		if (this.character != null) {
			updateTrigger ();
		}
	}

	public void initVariables(Character currCharacter){
		this.character = currCharacter;
	}

	public override void TriggerEffect(){
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
