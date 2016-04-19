using UnityEngine;
using System.Collections;

public class GhostItem : Item {
    //stuns enemies and characters

    public AudioClip audioEffectCharacterAsGhost;

    public int points;
    public GhostItem()
    {
        points = 10;
    }

    float timer = 20f; 
	Character character;
	//Character characters[];//holds all other characters
	
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		///base.Update ();
        if (base.beenTriggered == true)
        {
            updateTrigger();
        }
    }
	
	public void initVariables(Character currCharacter){
		character = currCharacter;
	}
	
	public override void TriggerEffect(){
        //ask Ian how he is doing the character constants (perhaps need a constants page)
        //save last button press and continue direction of characters in here by going through a for loop of character array
		base.TriggerEffect ();
        this.character.animator.SetBool("ghost", true);
        this.character.invincible = true;
        //this.character.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f);

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioEffectCharacterAsGhost = (AudioClip)Resources.Load("Ghost") as AudioClip;
        audioSource.PlayOneShot(audioEffectCharacterAsGhost, 0.7f);

    }

    void updateTrigger()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            this.character.invincible = false;
            this.character.animator.SetBool("ghost", false);
            //this.character.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            
            base.DestroySelf();
            Destroy(this.gameObject);
        }
    }
}
