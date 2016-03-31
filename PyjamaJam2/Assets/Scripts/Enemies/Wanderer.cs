using UnityEngine;
using System.Collections;

public class Wanderer : Enemy {

	public GameObject rockPrefab;

	public float lifeSpan = 40f; //in seconds

	float timer = 3.8f; //in seconds

	float lastPosX;
	float lastPosY;

	// Use this for initialization
	void Start () {
		lastPosX = transform.position.x + 0.2f;
		lastPosY = transform.position.y + 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = 3.8f;
			Instantiate (rockPrefab, new Vector3(lastPosX,lastPosY, -8) , Quaternion.identity);
		}

		lifeSpan -= Time.deltaTime;
		
		if (lifeSpan < 0)
		{
			destroySelf();
		}

	}

	public void destroySelf()
	{
		if (gameObject != null) 
			Destroy(gameObject);
	}
}
