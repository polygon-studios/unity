using UnityEngine;
using System.Collections;

public class Chili : Item {
	//all other characters can't stop running

	int effectTimer; 
	Character character;
	float timer = 10; //in seconds
	//Character characters[];//holds all other characters

    public AudioClip audioEffectFire; 

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		//base.Update ();
		if (base.beenTriggered == true) {
			updateTrigger();
		}
	}

	public void initVariables(Character currCharacter){
		character = currCharacter;
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioEffectFire = (AudioClip)Resources.Load("Chili") as AudioClip;
        audioSource.PlayOneShot(audioEffectFire, 0.25f);
	}

    public override void TriggerEffect()
    {
		base.TriggerEffect ();
        timer -= Time.deltaTime;
        foreach (Character currChar in base.GM.CHARACTERS)
        {
            if (currChar != character && currChar != null)
            {
                if (currChar.isStunned == true)
                {
                    currChar.isStunned = false;
                    currChar.animator.SetBool("stun", false);
                }
            }
        }
    }

	void updateTrigger(){
		timer -= Time.deltaTime;

        //item is done
        if (timer < 0)
        {
            //character.currentJump = character.starterJump;
            foreach (Character currChar in base.GM.CHARACTERS)
            {
                if (currChar != character && currChar != null)
                {
                    Debug.Log(currChar);
                    currChar.animator.SetBool("fire", false);
                }
            }

            base.allItems.removeItemFromArray(this.gameObject);
            //base.DestroySelf();
            Destroy(this.gameObject);
        }
        else { //chili is in use
            foreach (Character currChar in base.GM.CHARACTERS)
            {
                if (currChar != character && currChar != null)
                {
                    currChar.animator.SetBool("fire", true);
                    if (currChar.lastPressedKey == currChar.inputRight)
                    {
                        currChar.transform.Translate(currChar.currentSpeed * Time.deltaTime, 0.0f, 0.0f);
                        currChar.transform.eulerAngles = new Vector2(0, 0);
                    }
                    else if (currChar.lastPressedKey == currChar.inputLeft)
                    {
                        currChar.transform.Translate(currChar.currentSpeed * Time.deltaTime, 0.0f, 0.0f);
                        currChar.transform.eulerAngles = new Vector2(0, 180);
                    }
                }
            }
        }
	}
}
