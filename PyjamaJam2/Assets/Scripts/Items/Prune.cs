using UnityEngine;
using System.Collections;

public class Prune : Item {
	// all other characters grow old

	int effectTimer; 
	Character character;
	float timer = 20; //in seconds
	//Character characters[];//holds all other characters
	
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (base.beenTriggered == true)
        {
            updateTrigger();
        }
    }
	
	public void initVariables(Character currCharacter){
		character = currCharacter;
	}
	
	public override void TriggerEffect(){
        timer -= Time.deltaTime;
        foreach (Character currChar in base.GM.CHARACTERS)
        {
            if (currChar != character && currChar != null)
            {
                currChar.animator.SetBool(currChar.charID + "Old", true);
            }
        }
    }
	
	void updateTrigger(){
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            //character.currentJump = character.starterJump;
            foreach (Character currChar in base.GM.CHARACTERS)
            {
                if (currChar != character && currChar != null)
                {
                    currChar.animator.SetBool(currChar.charID + "Old", false);
                }
            }

            base.allItems.removeItemFromArray(this.gameObject);
            base.DestroySelf();
            Destroy(this.gameObject);
        }

       
    }
}
