using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Traps : MonoBehaviour {
	
	public static Traps TRAPS;
	public List<GameObject> TRAPARRAY = new List<GameObject>();
	
	void Awake(){
		if (TRAPS != null)
			GameObject.Destroy (TRAPS);
		else
			TRAPS = this;
		DontDestroyOnLoad (this);
	}
	
	
	public void removeItemFromArray(GameObject itemObj){
		TRAPARRAY.Remove (itemObj);
	}
}
