using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firework : Item {

    public List<GameObject> fireworks;
	float totalIntervalTimer = 10f; //seconds. //when this timer is done no more fireworks spawn
	float endTimer = 20f; //needs to be the largest of the three. When this timer is done the item is deleted
	float timerInterval = 0.2f; //seconds //new firework every 20ms
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
		endTimer -= Time.deltaTime;
		timerInterval -= Time.deltaTime;
		totalIntervalTimer -= Time.deltaTime;

		/*AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		AudioClip audioEffectFirework = (AudioClip)Resources.Load("Fireworks") as AudioClip;
		audioSource.PlayOneShot(audioEffectFirework, 0.9f);*/
	}
	
	void updateTrigger(){
		endTimer -= Time.deltaTime;
		timerInterval -= Time.deltaTime;
		totalIntervalTimer -= Time.deltaTime;

		if (timerInterval < 0 && totalIntervalTimer > 0) {
			float xPosition = Random.Range (2f, 27f);
			float yPosition = Random.Range (3f, 6f);

			GameObject firework = (GameObject)Instantiate (fireworks[Random.Range(0, fireworks.Count)], new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
            
            


			timerInterval = 0.5f;
		}

		if (endTimer < 0) {
			base.DestroySelf ();
			Destroy (this.gameObject);

		}
	}
}
