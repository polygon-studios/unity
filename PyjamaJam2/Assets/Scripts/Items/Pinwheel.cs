using UnityEngine;
using System.Collections;

public class Pinwheel : Item {
	public GameObject windPrefab;

	Character character;
	float timer = 5f;//in seconds
	Vector2 force = new Vector2 (4, 0);

	GameObject wind1;
	GameObject wind2;

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
		timer -= Time.deltaTime;

		wind1 = (GameObject)Instantiate (windPrefab, new Vector3 (22.0f, 4.0f, -2.4f), Quaternion.identity);
		wind2 = (GameObject)Instantiate (windPrefab, new Vector3 (8.0f, 4.0f, -2.4f), Quaternion.identity);

		foreach (GameObject item in base.allItems.lev1ItemsCurrent) {
			if(item != null){
				Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
				rb.AddForce(force, ForceMode2D.Impulse);
			}
		}foreach (GameObject item in base.allItems.lev2ItemsCurrent) {
			if(item != null){
				Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
				rb.AddForce(force, ForceMode2D.Impulse);
			}
		}foreach (GameObject item in base.allItems.lev3ItemsCurrent) {
			if(item != null){
				Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
				rb.AddForce(force, ForceMode2D.Impulse);
			}
		}

	}
	void updateTrigger(){
		timer -= Time.deltaTime;
		Debug.Log ("YES");
		if (timer < 0) {
			Destroy(wind1);
			Destroy(wind2);
			base.allItems.removeItemFromArray(this.gameObject);
			base.DestroySelf ();
			Destroy (this.gameObject);
		}
	}

}
