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

	int maxEasyItems = 8;
	int maxMedItems = 6;
	int maxHardItems = 4;


	float timer = 20; //in seconds

	void Awake(){
		if (ITEMS != null)
			GameObject.Destroy (ITEMS);
		else
			ITEMS = this;
		DontDestroyOnLoad (this);
	}

	void Update(){
		timer -= Time.deltaTime;
		//Debug.Log (timer);
		Debug.Log ("HERE");
		if (timer < 0) {
			timer = 20;
			//checkTypes();
		}
	}

	void checkTypes(){
		int easyCount = lev1ItemsCurrent.Count;
		int medCount = lev2ItemsCurrent.Count;
		int hardCount = lev3ItemsCurrent.Count;

		if (lev1ItemsCurrent.Count < maxEasyItems) {
			GameObject itemObj = (GameObject)Instantiate (lev1ItemsPrefabs[0], new Vector3 (15, 4.0f, 0), Quaternion.identity);
			if(itemObj.gameObject.GetComponent<Fish> () != null)

			ITEMSARRAY.Add (itemObj);
		}

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

	public void addItemToArray(GameObject itemObj){
		ITEMSARRAY.Add (itemObj);
	}
	
	public void removeItemFromArray(GameObject itemObj){
		ITEMSARRAY.Remove (itemObj);
	}
}
