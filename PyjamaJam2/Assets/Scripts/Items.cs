using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items : MonoBehaviour {

	public static Items ITEMS;
	public List<GameObject> ITEMSARRAY = new List<GameObject>();

	void Awake(){
		if (ITEMS != null)
			GameObject.Destroy (ITEMS);
		else
			ITEMS = this;
		DontDestroyOnLoad (this);
	}

	
	public void removeItemFromArray(GameObject itemObj){
		ITEMSARRAY.Remove (itemObj);

	}
}
