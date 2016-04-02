using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : Item {
    public AudioClip audioEffectFloppyFish;
	Character character;
    
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
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioEffectFloppyFish = (AudioClip)Resources.Load("Fish") as AudioClip;
        audioSource.PlayOneShot(audioEffectFloppyFish, 1.0f);
	}
	
	public override void TriggerEffect(){
		base.TriggerEffect ();
		foreach (Character currChar in base.GM.CHARACTERS) {
			if(currChar != character && currChar != null){
                currChar.isFishedTrigger();
			}
		}

        base.DestroySelf();
        base.allItems.removeItemFromArray(this.gameObject);
        Destroy(this.gameObject);
    }
}
