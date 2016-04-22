using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseMaster : MonoBehaviour {

	public GameObject interior;
	public GameObject exterior;
	public GameObject laurels;
	public List<GameObject> sleepingCharacters;

	public List<Vector3> sleepingPositions;
	public GameObject jailBars;
	bool endGame = false;

	SpriteRenderer extSpriteRenderer;

	// Use this for initialization
	void Start () {
		extSpriteRenderer = exterior.gameObject.GetComponent<SpriteRenderer> ();

		foreach (GameObject sleepingChar in sleepingCharacters) {
			sleepingChar.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		}
		laurels.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		jailBars.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void characterInsideHouse(List<Character>characters){
		if (endGame == false) {
			bool someoneIsInside = false;
			bool showJailbars = false;
			foreach (Character character in characters) {
				if (character.isInHouse == true)
					someoneIsInside = true;
				if (character.didPushTrapButton == true)
					showJailbars = true;
			}
			if (someoneIsInside == true)
				extSpriteRenderer.color = new Color (1f, 1f, 1f, 0f); //hide exterior
		else
				extSpriteRenderer.color = new Color (1f, 1f, 1f, 1f); //hide exterior


			//do the jailbars
			if (showJailbars == true)
				jailBars.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f); //hide exterior
		else
				jailBars.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f); //hide exterior

		}

	}

	public void setSleepingChars(string first, string second, string third, string fourth){
		foreach (GameObject sleepingChar in sleepingCharacters) {
			if (sleepingChar.name.Contains (first)) {
				sleepingChar.transform.position = sleepingPositions [0];
			}else if (sleepingChar.name.Contains (second)) {
				sleepingChar.transform.position = sleepingPositions [1];
			}else if (sleepingChar.name.Contains (third)) {
				sleepingChar.transform.position = sleepingPositions [2];
			}else if (sleepingChar.name.Contains (fourth)) {
				sleepingChar.transform.position = sleepingPositions [3];
			}
			sleepingChar.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			endGame = true;
		}
	}

	public void revealInterior(){
		laurels.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		interior.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 7;
		extSpriteRenderer.color = new Color (1f, 1f, 1f, 0f); //hide exterior
	}
}
