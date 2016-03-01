using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : Item {
	//other characters turn to fish
	public GameObject floppingFishPrefab;
	public GameMaster GM;

	Character character;
	List<GameObject> floppingFishes;

	int effectTimer; 
	float timer = 20; //in seconds
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (base.beenTriggered == true) {
			updateTrigger();
		}
	}
	
	public void initVariables(Character currCharacter){
		character = currCharacter;
	}
	
	public override void TriggerEffect(){
		timer -= Time.deltaTime;
		floppingFishes = new List<GameObject>();
		foreach (Character currChar in GM.CHARACTERS) {
			if(currChar != character && currChar != null){
				floppingFishes.Add((GameObject)Instantiate (floppingFishPrefab, new Vector3 (currChar.transform.position.x, currChar.transform.position.y, 0), Quaternion.identity));
				currChar.gameObject.GetComponent<Renderer>().enabled = false;
				currChar.gameObject.SetActive(false);
			}
		}
	}
	
	void updateTrigger(){
		timer -= Time.deltaTime;
		
		if (timer < 0) {

			foreach(GameObject floppingFish in floppingFishes){
				if(floppingFish != null)
					Destroy(floppingFish.gameObject);
			}

			foreach (Character currChar in GM.CHARACTERS) {
				if(currChar != character && currChar != null){
					currChar.gameObject.SetActive(true);
					currChar.gameObject.GetComponent<Renderer>().enabled = true;
				}
			}

			base.DestroySelf();
			base.allItems.removeItemFromArray(this.gameObject);
			Destroy(this.gameObject);
		}
	}
}
