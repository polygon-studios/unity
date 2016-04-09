using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : Item {
    public AudioClip audioEffectFloppyFish;
	Character character;
	float timer = 3f;
    public int points;
    public Fish()
    {
        points = 30;
    }
    
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

		base.TriggerEffect ();

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioEffectFloppyFish = (AudioClip)Resources.Load("Fish") as AudioClip;
        audioSource.PlayOneShot(audioEffectFloppyFish, 3.0f);

		foreach (Character currChar in base.GM.CHARACTERS) {
			if(currChar != character && currChar != null){
                currChar.isFishedTrigger();
			}
		}
 
    }
    void updateTrigger()
    {
		timer -= Time.deltaTime;
		if (timer < 0) {
			base.DestroySelf ();
			Destroy (this.gameObject);
		}
        
    }
}
