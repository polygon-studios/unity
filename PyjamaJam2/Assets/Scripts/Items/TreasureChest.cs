using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreasureChest : Item {
	public GameObject coinPrefab;
	Character character;

    public AudioClip audioEffectCoinChest; 
	float timer = 10f; //seconds
	float timerInterval = 0.5f; //seconds

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
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioEffectCoinChest = (AudioClip)Resources.Load("Coin Chest") as AudioClip;
        audioSource.PlayOneShot(audioEffectCoinChest, 0.4f);
	}

	public override void TriggerEffect(){
		timer -= Time.deltaTime;
		timerInterval -= Time.deltaTime;
	}

	void updateTrigger(){
		timer -= Time.deltaTime;
		timerInterval -= Time.deltaTime;

		if (timerInterval < 0) {
            float xPosition = Random.Range (character.transform.position.x - 4.0f, character.transform.position.x + 4.0f);
			GameObject coin = (GameObject)Instantiate (coinPrefab, new Vector3 (xPosition, 8.0f, -0.6f), Quaternion.identity);

			Coin coinScript =  coin.GetComponent<Coin>();
			coinScript.lifeSpan = Random.Range(5f, 10f);

			timerInterval = 0.5f;
        }

		if (timer < 0) {
			base.allItems.removeItemFromArray(this.gameObject);
			base.DestroySelf ();
			Destroy (this.gameObject);
            
        }
	}
}
