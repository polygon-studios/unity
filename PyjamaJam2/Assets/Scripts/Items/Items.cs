using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items : MonoBehaviour {

	public static Items ITEMS;
	public List<GameObject> ITEMSARRAY = new List<GameObject>();
	public List<GameObject> lev1ItemsPrefabs = new List<GameObject> ();
	public List<GameObject> lev2ItemsPrefabs = new List<GameObject>();
	public List<GameObject> lev3ItemsPrefabs = new List<GameObject>();

	public List<GameObject> lev1ItemsCurrent = new List<GameObject>();
	public List<GameObject> lev2ItemsCurrent = new List<GameObject>();
	public List<GameObject> lev3ItemsCurrent = new List<GameObject>();
	public GameMaster GM;

	int maxEasyItemsInScene = 5;
	int maxMedItems = 6;
	int maxHardItems = 4;

	int maxEasyPositions;

	List<Vector2> easyItemPositions ;


	float timer = 5; //in seconds

	void Awake(){
		if (ITEMS != null)
			GameObject.Destroy (ITEMS);
		else
			ITEMS = this;
		DontDestroyOnLoad (this);

		fillArrayPositions ();
	}

	void Update(){
		timer -= Time.deltaTime;
		//Debug.Log (timer);
		Debug.Log ("HERE");
		if (timer < 0) {
			timer = 5;
			checkTypes();
		}
	}

	void checkTypes(){
		int easyCount = lev1ItemsCurrent.Count;
		int medCount = lev2ItemsCurrent.Count;
		int hardCount = lev3ItemsCurrent.Count;

		if (easyCount < maxEasyItemsInScene) {
			int randPos = -10;
			//Vector3 finalPos = new Vector3(0,0,0);
			bool randPosIsUnique= false;

			while(randPosIsUnique == false){
				randPos = Random.Range (0, maxEasyPositions); 
				//finalPos = new Vector3(easyItemPositions[randPos].x, easyItemPositions[randPos].y, 0);

				if(lev1ItemsCurrent.Count > 0){
					foreach(GameObject itmObj in lev1ItemsCurrent){
						if( easyItemPositions[randPos].x > itmObj.transform.position.x + 0.1 ||
						    easyItemPositions[randPos].x < itmObj.transform.position.x - 0.1 )
							randPosIsUnique = true;
						else{
							randPosIsUnique = false;
							break;
						}
					}	
				}else
					randPosIsUnique = true;
			}

			int randItem = Random.Range (0, lev1ItemsPrefabs.Count);

			GameObject itemObj = (GameObject)Instantiate (lev1ItemsPrefabs[randItem], new Vector3(easyItemPositions[randPos].x,easyItemPositions[randPos].y, 0) , Quaternion.identity);
			if(itemObj != null)
		 		lev1ItemsCurrent.Add (itemObj);
			

		Debug.Log ("easy: " + easyCount + "   med: " + medCount + "   hard: " + hardCount);

		/*if (easyCount < maxEasyItems) {
			GameObject easyItem = (GameObject)Instantiate (easyItemsPrefabs[0], new Vector3 (15, 4.0f, 0), Quaternion.identity);
			addItemToArray(easyItem);

			if(GM.isDark == true){
				Item itemScript = easyItem.GetComponent<Item>();
				itemScript.setDarkMode();
			}
		}*/
		}

	}

	public void addItemToArray(GameObject itemObj){
		ITEMSARRAY.Add (itemObj);
	}
	
	public void removeItemFromArray(GameObject itemObj){
		ITEMSARRAY.Remove (itemObj);
	}

	void fillArrayPositions(){
		easyItemPositions = new List<Vector2> ();
		easyItemPositions.Add(new Vector2(7, 0));
		easyItemPositions.Add(new Vector2(8.35f, 0));
		easyItemPositions.Add(new Vector2(10, 0));
		easyItemPositions.Add(new Vector2(11.39f, 0));
		easyItemPositions.Add(new Vector2(18.75f, 0));
		easyItemPositions.Add(new Vector2(20.2f, 0));
		easyItemPositions.Add(new Vector2(21.63f, 0));

		easyItemPositions.Add(new Vector2(7.2f, 2.1f));
		easyItemPositions.Add(new Vector2(9, 2.1f));
		easyItemPositions.Add(new Vector2(10.5f, 2.1f));
		easyItemPositions.Add(new Vector2(19.1f, 2.1f));
		easyItemPositions.Add(new Vector2(20.7f, 2.1f));
		maxEasyPositions = easyItemPositions.Count;
	}

}
