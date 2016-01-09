using UnityEngine;
using System.Collections;

public class Slippers : Item{

	int effectTimer; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TriggerEffect(Character character){
		character.currentJump = 500f;
	}
}
