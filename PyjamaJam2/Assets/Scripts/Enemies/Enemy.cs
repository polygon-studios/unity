﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float lifeTime;

	public List<GameObject> enemyPrefs = new List<GameObject> ();

	List<GameObject> currentEnemies = new List<GameObject>();

	List<Vector2> dugPositions ;
	List<Vector2> charlesPositions ;
	List<Vector2> gruntPositions ;

	float timer = 30; //in seconds

	int spawns;
	int deaths;

	// Use this for initialization
	void Start () {
		spawns = 0;
		deaths = 0;

		fillArrayPositions ();
		currentEnemies.Add(generateEnemy(dugPositions, enemyPrefs, 0));
		currentEnemies.Add(generateEnemy(charlesPositions, enemyPrefs, 1));
	}
	
	// Update is called once per frame
	void Update () {
		bool charlesExists = false;
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = 30;

			int enemyCount = getCount(currentEnemies);

			if (enemyCount < 2)
            {
				for(int i = 0; i < currentEnemies.Count; i++) {
					if(currentEnemies[i].name.Contains("Charles")){
						charlesExists = true;
					}	
				}
				if(charlesExists){
                	currentEnemies.Add(generateEnemy(dugPositions, enemyPrefs, 0));
				}else {
					currentEnemies.Add(generateEnemy(charlesPositions, enemyPrefs, 1));
				}
            }
		}
	
	}

	int getCount(List<GameObject> listToCount)
	{
		int counter = 0;
		foreach(GameObject obj in listToCount)
		{
			if (obj != null)
				counter++;
		}
		return counter;
	}

	GameObject generateEnemy(List<Vector2> possiblePositions, List<GameObject> enemyPrefabs, int enemy){
		
		int randPos = -10;

		randPos = Random.Range (0, possiblePositions.Count); 
			

		
		GameObject itemObj = (GameObject)Instantiate (enemyPrefabs[enemy], new Vector3(possiblePositions[randPos].x,possiblePositions[randPos].y, -8) , Quaternion.identity);
		if (itemObj != null)
			return itemObj; 
		return null;
	}

	void fillArrayPositions(){

		// Possible dug placements
		dugPositions = new List<Vector2> ();
		dugPositions.Add(new Vector2(12.5f, 4.7f));
		dugPositions.Add(new Vector2(2.68f, 5.72f));
		dugPositions.Add(new Vector2(21.8f,4.22f));
		dugPositions.Add(new Vector2(26.39f, 2.14f));

		// Possible charles placements
		charlesPositions = new List<Vector2> ();
		charlesPositions.Add(new Vector2(19.36f, 2.45f));
		charlesPositions.Add(new Vector2(5.65f, 3.95f));
		
		// Possible grunt positions
		gruntPositions = new List<Vector2> ();
		gruntPositions.Add(new Vector2(0.42f, 0));
		gruntPositions.Add(new Vector2(1.18f, 0));
		gruntPositions.Add(new Vector2(1.88f, 0));
		gruntPositions.Add(new Vector2(2.6f, 0));
		gruntPositions.Add(new Vector2(26.36f, 0));

	}
}
