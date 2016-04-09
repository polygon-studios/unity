using UnityEngine;
using System.Collections;

public class Slippers : Item{

	int effectTimer; 
	Character character;
	float timer = 10; //in seconds
	float newHeightJump = 500f;
    public int points;
    public Slippers()
    {
        points = 20;
    }

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		//base.Update ();
 		if (base.beenTriggered == true) {
			updateTrigger ();
		}
	}

	public void initVariables(Character currCharacter){
		this.character = currCharacter;
	}

	public override void TriggerEffect(){
		base.TriggerEffect ();
		this.character.currentJump = newHeightJump;
		timer -= Time.deltaTime;
		this.character.animator.SetBool ("slippers", true);
        
	}

	void updateTrigger(){
		timer -= Time.deltaTime;

   		if (timer < 0) {
			this.character.animator.SetBool ("slippers", false);
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
