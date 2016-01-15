using UnityEngine;
using System.Collections;

public class Pinwheel : Item {
	Character character;
	float timer = 200f;//in seconds

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TriggerEffect(Character currCharacter){
		this.character = currCharacter;
		timer -= Time.deltaTime;
		
	}
	
	void updateTrigger(){
		timer -= Time.deltaTime;
		
		if (timer < 0) {
			Reset();
		}
	}
	
	void Reset(){
		//destroy from existence
		Destroy(gameObject);
	}

}
