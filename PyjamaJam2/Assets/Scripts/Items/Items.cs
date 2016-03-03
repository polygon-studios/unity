using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items : MonoBehaviour {

	public static Items ITEMS;
	//public List<GameObject> ITEMSARRAY = new List<GameObject>();
	public List<GameObject> lev1ItemsPrefabs = new List<GameObject> ();
	public List<GameObject> lev2ItemsPrefabs = new List<GameObject>();
	public List<GameObject> lev3ItemsPrefabs = new List<GameObject>();

	public List<GameObject> lev1ItemsCurrent = new List<GameObject>();
	public List<GameObject> lev2ItemsCurrent = new List<GameObject>();
	public List<GameObject> lev3ItemsCurrent = new List<GameObject>();
	public GameMaster GM;

	int maxEasyItemsInScene = 5;
	int maxMedItemsInScene = 6;
	int maxHardItemsInScene = 4;

	List<Vector2> easyItemPositions ;
	List<Vector2> mediumItemPositions ;
	List<Vector2> hardItemPositions ;

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
			lev1ItemsCurrent.Add (generateItems(easyItemPositions, lev1ItemsCurrent, lev1ItemsPrefabs));
		}
		if (medCount < maxMedItemsInScene) {
			lev2ItemsCurrent.Add (generateItems(mediumItemPositions, lev2ItemsCurrent, lev2ItemsPrefabs));
		}
		if (hardCount < maxHardItemsInScene) {
			lev3ItemsCurrent.Add (generateItems(hardItemPositions, lev3ItemsCurrent, lev3ItemsPrefabs));
		}

	}

	GameObject generateItems(List<Vector2> possibleItemPositions, List<GameObject> currentItemsInPlay, List<GameObject> prefabsToChoose){

		int randPos = -10;
		bool randPosIsUnique= false;
		
		while(randPosIsUnique == false){
			randPos = Random.Range (0, possibleItemPositions.Count); 
			//finalPos = new Vector3(easyItemPositions[randPos].x, easyItemPositions[randPos].y, 0);
			
			if(currentItemsInPlay.Count > 0){
				foreach(GameObject itmObj in currentItemsInPlay){
					if( possibleItemPositions[randPos].x > itmObj.transform.position.x + 0.1 ||
					   possibleItemPositions[randPos].x < itmObj.transform.position.x - 0.1 )
						randPosIsUnique = true;
					else{
						randPosIsUnique = false;
						break;
					}
				}	
			}else
				randPosIsUnique = true;
		}
		
		int randItem = Random.Range (0, prefabsToChoose.Count);
		
		GameObject itemObj = (GameObject)Instantiate (prefabsToChoose[randItem], new Vector3(possibleItemPositions[randPos].x,possibleItemPositions[randPos].y, 0) , Quaternion.identity);
		if (itemObj != null)
			return itemObj; 
		return null;
	}
	
	public void addItemToArray(GameObject itemObj){
		//ITEMSARRAY.Add (itemObj);
	}
	
	public void removeItemFromArray(GameObject itemObj){
		//ITEMSARRAY.Remove (itemObj);
		if (itemObj.GetComponent<Prune> () != null) {
			lev1ItemsCurrent.Remove(itemObj);
		}if (itemObj.GetComponent<Chili> () != null) {
			lev1ItemsCurrent.Remove(itemObj);
		}if (itemObj.GetComponent<Fish> () != null) {
			lev1ItemsCurrent.Remove(itemObj);
		}
		else if (itemObj.GetComponent<Slippers> () != null) {
			lev2ItemsCurrent.Remove(itemObj);
		}else if (itemObj.GetComponent<Pinwheel> () != null) {
			lev2ItemsCurrent.Remove(itemObj);
		}else if (itemObj.GetComponent<GhostItem> () != null) {
			lev2ItemsCurrent.Remove(itemObj);
		}
		else if (itemObj.GetComponent<TreasureChest> () != null) {
			lev2ItemsCurrent.Remove(itemObj);
		}else if (itemObj.GetComponent<Firework> () != null) {
			lev2ItemsCurrent.Remove(itemObj);
		}
	}

	public List<GameObject> getEasyItems(){
		return lev1ItemsCurrent;
	}

	public List<GameObject> getMedItems(){
		return lev2ItemsCurrent;
	}

	public List<GameObject> getHardItems(){
		return lev3ItemsCurrent;
	}

	void fillArrayPositions(){
		///// easy Item Positions
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

		/////medium Item Positions
		mediumItemPositions = new List<Vector2> ();
		mediumItemPositions.Add(new Vector2(3.3f, 0));
		mediumItemPositions.Add(new Vector2(4, 0));
		mediumItemPositions.Add(new Vector2(4.7f, 0));
		mediumItemPositions.Add(new Vector2(5.4f, 0));
		mediumItemPositions.Add(new Vector2(6.1f, 0));
		mediumItemPositions.Add(new Vector2(22.8f, 0));
		mediumItemPositions.Add(new Vector2(23.5f, 0));
		mediumItemPositions.Add(new Vector2(24.2f, 0));
		mediumItemPositions.Add(new Vector2(24.9f, 0));
		mediumItemPositions.Add(new Vector2(25.6f, 0));

		mediumItemPositions.Add(new Vector2(24.9f, 1.5f));
		mediumItemPositions.Add(new Vector2(25.6f, 1.5f));

		mediumItemPositions.Add(new Vector2(4.1f, 2.2f));
		mediumItemPositions.Add(new Vector2(5.5f, 2.2f));
		mediumItemPositions.Add(new Vector2(6.3f, 2.2f));

		mediumItemPositions.Add(new Vector2(25f, 3f));

		mediumItemPositions.Add(new Vector2(3.34f, 3.7f));
		mediumItemPositions.Add(new Vector2(5.5f, 3.7f));
		mediumItemPositions.Add(new Vector2(6.3f, 3.7f));
		mediumItemPositions.Add(new Vector2(17f, 3.7f));
		mediumItemPositions.Add(new Vector2(17.7f, 3.7f));
		mediumItemPositions.Add(new Vector2(18.55f, 3.7f));
		mediumItemPositions.Add(new Vector2(22f, 3.7f));
		mediumItemPositions.Add(new Vector2(22.5f, 3.7f));

		mediumItemPositions.Add(new Vector2(8.4f, 4.4f));
		mediumItemPositions.Add(new Vector2(9.9f, 4.4f));
		mediumItemPositions.Add(new Vector2(12.7f, 4.4f));
		mediumItemPositions.Add(new Vector2(13.4f, 4.4f));
		mediumItemPositions.Add(new Vector2(14.15f, 4.4f));
		mediumItemPositions.Add(new Vector2(20.6f, 4.4f));

		mediumItemPositions.Add(new Vector2(3.41f, 5f));
		mediumItemPositions.Add(new Vector2(3.7f, 5f));
		mediumItemPositions.Add(new Vector2(19.13f, 5f));
		mediumItemPositions.Add(new Vector2(24.85f, 5f));
		mediumItemPositions.Add(new Vector2(5.08f, 5f));

		////hard Item Positions
		hardItemPositions = new List<Vector2> ();
		hardItemPositions.Add(new Vector2(0.42f, 0));
		hardItemPositions.Add(new Vector2(1.18f, 0));
		hardItemPositions.Add(new Vector2(1.88f, 0));
		hardItemPositions.Add(new Vector2(2.6f, 0));
		hardItemPositions.Add(new Vector2(26.36f, 0));
		hardItemPositions.Add(new Vector2(27.17f, 0));
		hardItemPositions.Add(new Vector2(27.9f, 0));
		hardItemPositions.Add(new Vector2(28.7f, 0));

		hardItemPositions.Add(new Vector2(0.42f, 1.5f));
		hardItemPositions.Add(new Vector2(2.64f, 1.5f));
		hardItemPositions.Add(new Vector2(26.36f, 1.5f));

		hardItemPositions.Add(new Vector2(1.89f, 3f));
		hardItemPositions.Add(new Vector2(28.7f, 3f));

		mediumItemPositions.Add(new Vector2(26.37f, 3.7f));
		mediumItemPositions.Add(new Vector2(27.1f, 3.7f));

		hardItemPositions.Add(new Vector2(1.15f, 5));
		hardItemPositions.Add(new Vector2(2.58f, 5));
		hardItemPositions.Add(new Vector2(27.8f, 5));

		hardItemPositions.Add(new Vector2(7.7f, 5.8f));
		hardItemPositions.Add(new Vector2(8.45f, 5.8f));
		hardItemPositions.Add(new Vector2(11.25f, 5.8f));
		hardItemPositions.Add(new Vector2(12f, 5.8f));
		hardItemPositions.Add(new Vector2(12.8f, 5.8f));
		hardItemPositions.Add(new Vector2(22.7f, 5.8f));
		hardItemPositions.Add(new Vector2(23.4f, 5.8f));

		hardItemPositions.Add(new Vector2(0.42f, 6.5f));
		hardItemPositions.Add(new Vector2(1.22f, 6.5f));
		hardItemPositions.Add(new Vector2(2.65f, 6.5f));
		hardItemPositions.Add(new Vector2(4f, 6.5f));
		hardItemPositions.Add(new Vector2(6.25f, 6.5f));
		hardItemPositions.Add(new Vector2(9.86f, 6.5f));
		hardItemPositions.Add(new Vector2(14.15f, 6.5f));
		hardItemPositions.Add(new Vector2(14.88f, 6.5f));
		hardItemPositions.Add(new Vector2(17f, 6.5f));
		hardItemPositions.Add(new Vector2(19.18f, 6.5f));
		hardItemPositions.Add(new Vector2(20.64f, 6.5f));
		hardItemPositions.Add(new Vector2(25.7f, 6.5f));
		hardItemPositions.Add(new Vector2(27f, 6.5f));
		hardItemPositions.Add(new Vector2(28.6f, 6.5f));
	}

}
