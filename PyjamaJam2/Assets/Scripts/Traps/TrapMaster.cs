using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapMaster : MonoBehaviour {
	
	public List<GameObject> currentTraps = new List<GameObject>();

	public GameObject buttonPrefab;
	public GameObject bramblePrefab;
	public GameObject pineconePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public void generateTrap(string trapName, float xPos, float yPos, int ID){
		GameObject trap;
		if (trapName.Contains ("bramble")) {
			trap = (GameObject)Instantiate (bramblePrefab, new Vector3 (xPos, yPos, -7), Quaternion.identity);
		} else if (trapName.Contains("pinecone")) {
			trap = (GameObject)Instantiate (pineconePrefab, new Vector3 (xPos, yPos, -7), Quaternion.identity);
		}
		else {
			trap = (GameObject)Instantiate(buttonPrefab, new Vector3(xPos, yPos, -7), Quaternion.identity);
		}
		currentTraps.Add (trap);
	}

	public void removeItemFromArray(GameObject itemObj){
		currentTraps.Remove(itemObj);
	}
}
