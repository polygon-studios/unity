using UnityEngine;
using System.Collections;

public class Dug: Enemy {

	Transform target; //the enemy's target
	public float moveSpeed= 3; //move speed
	public float rotationSpeed= 3; //speed of turning

	public float lifeSpan = 60f; //in seconds

	public GameObject[] characters;
	
	
	Transform enemyTransform; //current transform data of this enemy
	
	// Initial X position
	float initialX;
	float lastPosX;
	float lastPosY;
	float actualXSpeed;
	float actualYSpeed;

	void Awake() {
		enemyTransform = transform; //cache transform data for easy access/preformance
	}
	
	
	void Start() {
		target = GameObject.FindWithTag("character").transform; //target the player
		// Store initial y
		initialX = enemyTransform.position.x;
		lastPosX = enemyTransform.position.x;
		lastPosY = enemyTransform.position.y;
	}
	
	
	void Update () {
		characters = GameObject.FindGameObjectsWithTag("character"); //target the player


		foreach (GameObject character in characters) {
			target = character.transform;

			// Reflect the sprite if the enemy changes directions
			if (lastPosX > enemyTransform.position.x) {
				Vector3 theScale = transform.localScale;
				theScale.x = -0.42511f;
				transform.localScale = theScale;
				lastPosX = enemyTransform.position.x;
			}
			else {
				Vector3 theScale = transform.localScale;
				theScale.x = 0.42511f;
				transform.localScale = theScale;
				lastPosX = enemyTransform.position.x;
			}
			
			// Determine if negative movespeed is required
			if (target.position.x < enemyTransform.position.x) {
				actualXSpeed = -moveSpeed;
			} else {
				actualXSpeed = moveSpeed;
			}
			
			// Determine if negative movespeed is required
			if (target.position.y < enemyTransform.position.y) {
				actualYSpeed = -moveSpeed;
			} else {
				actualYSpeed = moveSpeed;
			}
			
			
			if (Vector3.Distance(enemyTransform.position,target.position)<4f){
				transform.Translate(new Vector3(actualXSpeed* Time.deltaTime,actualYSpeed* Time.deltaTime,0) );
				//Debug.Log("Moving towards: " + target.position);
			}
			else {
				Vector3 pos = transform.position;
				
				// Oscillate back and forth between the initial x and 5 units
				
				pos.y = Mathf.PingPong(Time.time*1, 5) + lastPosY;
			}
		}



		lifeSpan -= Time.deltaTime;
		
		if (lifeSpan < 0)
		{
			destroySelf();
		}
		
	}

	public void destroySelf()
	{
		if (gameObject != null) { 
			base.removeItemFromArray(this.gameObject);
			base.destroySelf();
			Destroy (this.gameObject);
		}
	}
}
