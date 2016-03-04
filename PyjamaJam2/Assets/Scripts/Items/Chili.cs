using UnityEngine;
using System.Collections;

public class Chili : Item {
	//all other characters can't stop running

	int effectTimer; 
	Character character;
	float timer = 20; //in seconds
	//Character characters[];//holds all other characters


	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		//base.Update ();
		if (base.beenTriggered == true) {
			updateTrigger();
		}
	}

	public void initVariables(Character currCharacter){
		character = currCharacter;
	}

	public override void TriggerEffect(){
		timer -= Time.deltaTime;
	}

	void updateTrigger(){
		timer -= Time.deltaTime;
		
		if (timer < 0) {
			character.currentJump = character.starterJump;
			base.allItems.removeItemFromArray(this.gameObject);
			base.DestroySelf();
			Destroy(this.gameObject);
		}

		foreach (Character currChar in base.GM.CHARACTERS) {
			if(currChar != character && currChar != null){
				if(currChar.lastPressedKey == currChar.inputRight){
					currChar.transform.Translate (currChar.speed * Time.deltaTime, 0.0f, 0.0f);
					currChar.transform.eulerAngles = new Vector2 (0, 0);
				}else if (currChar.lastPressedKey == currChar.inputLeft){
					currChar.transform.Translate(currChar.speed * Time.deltaTime, 0.0f, 0.0f);
					currChar.transform.eulerAngles = new Vector2(0, 180);
				}
			}
		}
	}
}
