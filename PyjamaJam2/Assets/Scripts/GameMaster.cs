using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster:MonoBehaviour
{
	public static GameMaster GM;
	public List<Character> CHARACTERS;
	public bool isDark;

    private bool showGUI = true;


    public int foxScore;
	public int skunkScore;
	public int bearScore;
	public int rabbitScore;
	public static float timer;
	public bool gameOver;

	void Awake(){
		if (GM != null)
			GameObject.Destroy (GM);
		else
			GM = this;
		DontDestroyOnLoad (this);
	}

	void Start(){

		GameObject[] charGO = GameObject.FindGameObjectsWithTag ("character");
		foreach (GameObject GO in charGO) {
			CHARACTERS.Add(GO.GetComponent<Character>());
		}

		foxScore = 0;
		skunkScore = 0;
		bearScore = 0;
		rabbitScore = 0;
	}

	void Update(){

		timer += Time.deltaTime;
	}

	void OnGUI() {
        //int minutes = Mathf.FloorToInt(timer / 60F);
        //int seconds = Mathf.FloorToInt(timer - minutes * 60);
        //string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        //niceTime = "Time: " + niceTime;
        //GUI.Label(new Rect(10, 10, 250, 100), niceTime);
        if (showGUI)
        {
            string fox = string.Format("{0000}", foxScore);
            GUI.Label(new Rect(574, 315, 250, 100), fox);

            string skunk = string.Format("{0000}", skunkScore);
            GUI.Label(new Rect(815, 315, 250, 100), skunk);

            string rabbit = string.Format("{0000}", rabbitScore);
            GUI.Label(new Rect(574, 355, 250, 100), rabbit);

            string bear = string.Format("{0000}", bearScore);
            GUI.Label(new Rect(815, 355, 250, 100), bear);
        }
	}

	public void addPoints(string character, int pointVal){
		Debug.Log ("ADDING SCORE");
		if (character.Contains ("Fox")) {
			foxScore += foxScore + pointVal;
		}
		if (character.Contains ("Skunk")) {
			skunkScore += skunkScore + pointVal;
		}
		if (character.Contains ("Bear")) {
			bearScore += bearScore + pointVal;
		}
		if (character.Contains ("Rabbit")) {
			rabbitScore += rabbitScore + pointVal;
		}
	}

}

