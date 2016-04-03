using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firework : Item {

    public List<GameObject> fireworks;
	float timer = 10f; //seconds
	float timerInterval = 0.2f; //seconds
    public AudioClip audioFireworkEffect;

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
		
	}
	
	public override void TriggerEffect(){
		base.TriggerEffect ();
		timer -= Time.deltaTime;
		timerInterval -= Time.deltaTime;
	}
	
	void updateTrigger(){
		timer -= Time.deltaTime;
		timerInterval -= Time.deltaTime;

		if (timerInterval < 0) {
			float xPosition = Random.Range (2f, 27f);
			float yPosition = Random.Range (3f, 6f);

			GameObject firework = (GameObject)Instantiate (fireworks[Random.Range(0, fireworks.Count)], new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
            
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioClip audioEffectFirework = (AudioClip)Resources.Load("Firework") as AudioClip;
            audioSource.PlayOneShot(audioEffectFirework, 0.9f);


			timerInterval = 0.5f;
		}

		if (timer < 0) {
			base.allItems.removeItemFromArray(this.gameObject);
			base.DestroySelf ();
			Destroy (this.gameObject);

		}
	}
}
