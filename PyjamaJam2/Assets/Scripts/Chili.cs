using UnityEngine;
using System.Collections;

public class Chili : Item {
	//all other characters can't stop running

	int effectTimer; 
	Character character;
	float timer = 20; //in seconds
	//Character characters[];//holds all other characters


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void TriggerEffect(){
		//ask Ian how he is doing the character constants (perhaps need a constants page)
		//save last button press and continue direction of characters in here by going through a for loop of character array

	}

	void updateTrigger(){

	}
}
