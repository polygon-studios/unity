using UnityEngine;
using System.Collections;

public class OilLatern : Item {
	
	int effectTimer; 
	Character character;
	//Character characters[];//holds all other characters
	
	
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
		base.TriggerEffect ();
		Latern laternScript = character.latern.GetComponent<Latern> ();
		laternScript.lightBoost ();

		base.allItems.removeItemFromArray(this.gameObject);
		base.DestroySelf();
		Destroy(this.gameObject);
	}
	
	void updateTrigger(){
		
	}
}
