using UnityEngine;
using System.Collections;

public class Pinwheel : Item {
	Character character;
	float timer = 200f;//in seconds
	Vector2 force = new Vector2 (4, 0);

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	
	}

	public void TriggerEffect(Character currCharacter){
		character = currCharacter;
		timer -= Time.deltaTime;

		foreach (GameObject item in base.allItems.ITEMSARRAY) {
			Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
			rb.AddForce(force, ForceMode2D.Impulse);
		}
	
	}
	
	void updateTrigger(){
		timer -= Time.deltaTime;
		
		if (timer < 0) {
			base.DestroySelf();
			Destroy(this.gameObject);
		}
	}

}
