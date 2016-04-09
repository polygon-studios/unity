using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dug: Enemy {

	Transform target; //the enemy's target
	public float moveSpeed= 3; //move speed
	public float rotationSpeed= 3; //speed of turning

	public float lifeSpan = 60f; //in seconds

	public GameObject[] characters;

	public List<GameObject> characterList = new List<GameObject>();
	
	
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
		characters = GameObject.FindGameObjectsWithTag("character"); //target the player

		foreach (GameObject character in characters) {
			characterList.Add(character);
		}
		
		// Store initial y
		initialX = enemyTransform.position.x;
		lastPosX = enemyTransform.position.x;
		lastPosY = enemyTransform.position.y;
	}
	
	
	void Update () {
		//characters = GameObject.FindGameObjectsWithTag("character"); //target the player


		foreach (GameObject character in characterList) {
			target = character.transform;

            if (Vector3.Distance(enemyTransform.position, target.position) < 4f && Vector3.Distance(enemyTransform.position, target.position) > 0.1f)
            {
				transform.Translate(new Vector3(actualXSpeed* Time.deltaTime,actualYSpeed* Time.deltaTime,0) );

				// Reflect the sprite if the enemy changes directions
				float deltaX = lastPosX - enemyTransform.position.x;
				if ((lastPosX > enemyTransform.position.x) && (deltaX > 0.010 || deltaX < -0.010)) {
					Vector3 theScale = transform.localScale;
					if(theScale.x > 0){
						theScale.x = theScale.x * -1;
						transform.localScale = theScale;
						lastPosX = enemyTransform.position.x;
					}
                    Debug.Log("Moving left with scale of: "+ theScale.x);
				}
                else if(lastPosX > enemyTransform.position.x) { 
                    Vector3 theScale = transform.localScale;
					if(theScale.x > 0){
						theScale.x = theScale.x * -1;
						transform.localScale = theScale;
						lastPosX = enemyTransform.position.x;
					}
                }
				else {
					Vector3 theScale = transform.localScale;
					if(theScale.x < 0f){
						theScale.x = theScale.x * -1;
					}
					theScale.x = theScale.x * 1;
					transform.localScale = theScale;
					lastPosX = enemyTransform.position.x;
                    Debug.Log("Moving left with scale of: " + theScale.x);
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
			//base.destroySelf();
			Destroy (gameObject);
		}
	}
}
