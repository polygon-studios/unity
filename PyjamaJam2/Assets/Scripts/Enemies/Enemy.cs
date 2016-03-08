using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float lifeTime;

	public List<GameObject> enemyPrefs = new List<GameObject> ();

	List<Vector2> dugPositions ;
	List<Vector2> charlesPositions ;
	List<Vector2> gruntPositions ;

	int spawns;
	int deaths;

	// Use this for initialization
	void Start () {
		spawns = 0;
		deaths = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		/*if (1 > 2) {
			generateEnemy(dugPositions, enemyPrefs, 0);
		}*/
	}

	/*GameObject generateEnemy(List<GameObject> possiblePositions, List<GameObject> enemyPrefabs, int enemy){
		
		int randPos = -10;

		randPos = Random.Range (0, possiblePositions.Count); 
			

		
		//GameObject itemObj = (GameObject)Instantiate (enemyPrefabs[enemy], new Vector3(possiblePositions[randPos].x,possiblePositions[randPos].y, 0) , Quaternion.identity);
		if (itemObj != null)
			return itemObj; 
		return null;
	}

	void fillArrayPositions(){

		// Possible dug placements
		dugPositions = new List<Vector2> ();
		dugPositions.Add(new Vector2(7, 0));
		dugPositions.Add(new Vector2(8.35f, 0));
		dugPositions.Add(new Vector2(10, 0));
		dugPositions.Add(new Vector2(11.39f, 0));

		// Possible charles placements
		charlesPositions = new List<Vector2> ();
		charlesPositions.Add(new Vector2(3.3f, 0));
		charlesPositions.Add(new Vector2(4, 0));
		charlesPositions.Add(new Vector2(4.7f, 0));
		charlesPositions.Add(new Vector2(5.4f, 0));
		charlesPositions.Add(new Vector2(6.1f, 0));
		charlesPositions.Add(new Vector2(22.8f, 0));
		
		// Possible grunt positions
		gruntPositions = new List<Vector2> ();
		gruntPositions.Add(new Vector2(0.42f, 0));
		gruntPositions.Add(new Vector2(1.18f, 0));
		gruntPositions.Add(new Vector2(1.88f, 0));
		gruntPositions.Add(new Vector2(2.6f, 0));
		gruntPositions.Add(new Vector2(26.36f, 0));

	}*/
}
