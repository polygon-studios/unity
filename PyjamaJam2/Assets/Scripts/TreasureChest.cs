using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreasureChest : Item {
	public GameObject coinPrefab;
	List<Object> coinsArray = new List<Object>();
	Character character;

	float timer = 5f; //seconds

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (base.beenTriggered == true) {
			updateTrigger ();
		}
	}

	public void initVariables(Character currCharacter){
		character = currCharacter;
	}

	public override void TriggerEffect(){
		float xPosition = Random.Range (character.transform.position.x - 6.0f, character.transform.position.x + 6.0f);
		Object coin = Instantiate (coinPrefab, new Vector3 (xPosition, 5.0f, 0), Quaternion.identity);
		coinsArray.Add (coin);

		timer -= Time.deltaTime;

	}

	void updateTrigger(){
		timer -= Time.deltaTime;
		
		if (timer < 0) {
			base.DestroySelf ();
			Destroy (this.gameObject);
		}
	}
}
