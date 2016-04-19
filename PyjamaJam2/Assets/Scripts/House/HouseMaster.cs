using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseMaster : MonoBehaviour {

	public GameObject interior;
	public GameObject exterior;
	public List<GameObject> sleepingCharacters;

	public List<Vector3> sleepingPositions;

	SpriteRenderer extSpriteRenderer;

	// Use this for initialization
	void Start () {
		extSpriteRenderer = exterior.gameObject.GetComponent<SpriteRenderer> ();

		foreach (GameObject sleepingChar in sleepingCharacters) {
			sleepingChar.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void characterInsideHouse(List<Character>characters){
		bool someoneIsInside = false;
		foreach (Character character in characters) {
			if (character.isInHouse == true)
				someoneIsInside = true;
		}
		if (someoneIsInside == true)
			extSpriteRenderer.color = new Color (1f, 1f, 1f, 0f); //hide exterior
		else
			extSpriteRenderer.color = new Color (1f, 1f, 1f, 1f); //hide exterior
	}

	public void setSleepingChars(string first, string second, string third, string fourth){
		foreach (GameObject sleepingChar in sleepingCharacters) {
			if (sleepingChar.name.Contains (first)) {
				sleepingChar.transform.position = sleepingPositions [0];
			}else if (sleepingChar.name.Contains (first)) {
				sleepingChar.transform.position = sleepingPositions [1];
			}else if (sleepingChar.name.Contains (first)) {
				sleepingChar.transform.position = sleepingPositions [2];
			}else if (sleepingChar.name.Contains (first)) {
				sleepingChar.transform.position = sleepingPositions [3];
			}
			sleepingChar.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}
}
