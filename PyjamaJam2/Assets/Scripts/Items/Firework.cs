using UnityEngine;
using System.Collections;

public class Firework : Item {

	Character character;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		
	}
	
	public void initVariables(Character currCharacter){
		character = currCharacter;
	}
	
	public override void TriggerEffect(){
		//ask Ian how he is doing the character constants (perhaps need a constants page)
		//save last button press and continue direction of characters in here by going through a for loop of character array
		base.allItems.removeItemFromArray(this.gameObject);
		base.DestroySelf();
		Destroy(this.gameObject);
	}
	
	void updateTrigger(){
		
	}
}
