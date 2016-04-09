/*
*   Charles.cs
*   
*   File that handles the Charle's movement
*   
*  
*   Author: Ian Clarke
*/

using UnityEngine;
using System.Collections;

public class Charles : Enemy {
	
	/*
    * Declarations
    */
	
	// Speed of enemy
	public float speed;
	public float distance;

	public float lifeSpan = 120f; //in seconds
	
	// Initial X position
	float initialX;
	float lastPosX;
	
	// Initializer
	void Start()
	{
		// Store initial y
		initialX = transform.position.x;
		lastPosX = transform.position.x;
	}
	
	// Updates game
	void Update()
	{
		movement ();
	}
	
	// Handes enemy movement
	void movement()
	{
		// Move back and forth 3 units
		Vector3 pos = transform.position;
		pos.x = Mathf.PingPong(Time.time * speed, distance) + initialX;
		transform.position = pos;

		// Reflect the sprite if the enemy changes directions
		if (lastPosX > pos.x)
		{
			Vector3 theScale = transform.localScale;
			theScale.x = -0.42511f;
			transform.localScale = theScale;
			transform.position = pos;
			lastPosX = pos.x;
		}
		else
		{
			Vector3 theScale = transform.localScale;
			theScale.x = 0.42511f;
			transform.localScale = theScale;
			transform.position = pos;
			lastPosX = pos.x;
		}
		
	}
}