using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CallingCharAnimations : MonoBehaviour {

	public List<GameObject> houseCharacters;
	public List<GameObject> sleepingCharacters;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	/*public void callAnimation(string charName, string side){
		foreach (GameObject houseCharactersObj in houseCharacters) {
			HouseCharacter houseChar = houseCharactersObj.GetComponent<>();
			Debug.Log("Trying to animate " + houseChar.name + " from the data: " + charName);


			if(charName.Contains (houseChar.name)){
				houseChar.resetAni();
				Debug.Log("it contains!s");
				if(side.Contains("right")){
					houseChar.rightToLeftPlaying = true;

					Debug.Log("Right");
				}else if (side.Contains("left")){
					houseChar.leftToRightPlaying = true;
					Debug.Log("Left");
				}
			}
		}
	}

	public void callAnimationWithItem(string charName, string side, string item){
		foreach (GameObject houseCharactersObj in houseCharacters)
		{
			HouseCharacter houseChar = houseCharactersObj.GetComponent<HouseCharacter>();
			Debug.Log("Trying to animate " + houseChar.name + " from the data: " + charName + " with: " + item);


			if (charName.Contains(houseChar.name))
			{
				houseChar.resetAni();
				houseChar.currentItem = item;
				Debug.Log("it contains!s");
				if (side.Contains("right"))
				{
					houseChar.rightToLeftBINDLEPlaying = true;
					Debug.Log("Right");
				}
				else if (side.Contains("left"))
				{
					houseChar.leftToRightBINDLEPlaying = true;
					Debug.Log("Left");
				}
			}
		}
	}


	public void sleepingAnimations(string first, string second, string third, string fourth){
		foreach (GameObject sleepingCharObj in sleepingCharacters) {
			SleepingChar sleepChar = sleepingCharObj.GetComponent<SleepingChar> ();
			int ranking = 5;

			//0,1,2,3 for conversion to match with an array

			if (first.Contains (sleepChar.charID))
				ranking = 0;
			else if (second.Contains (sleepChar.charID))
				ranking = 1;
			else if (third.Contains (sleepChar.charID))
				ranking = 2;
			else if (fourth.Contains (sleepChar.charID))
				ranking = 3;

			sleepChar.goSleeping (ranking);
		}
	}*/
}
