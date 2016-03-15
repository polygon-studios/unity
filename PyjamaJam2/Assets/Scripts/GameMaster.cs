using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster:MonoBehaviour
{
	public static GameMaster GM;
	public List<Character> CHARACTERS;
	public bool isDark;


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

		OnGUI ();
	}

	void OnGUI()
	{
		if (!gameOver)
		{
			int minutes = Mathf.FloorToInt(timer / 60F);
			int seconds = Mathf.FloorToInt(timer - minutes * 60);
			string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
			niceTime = "Time: " + niceTime;
			//GUI.Label(new Rect(10, 10, 250, 100), niceTime);
			
			string fox = "Fox: " + string.Format("{000}", foxScore);
			GUI.Label(new Rect(600, 230, 250, 100), fox);
			
			string skunk = "Skunk: " + string.Format("{000}", foxScore);
			GUI.Label(new Rect(600, 250, 250, 100), skunk);
			
			string bear = "Bear: " + string.Format("{000}", bearScore);
			GUI.Label(new Rect(600, 270, 250, 100), bear);
		}
		
		if (gameOver)
		{
			var centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 250, 100), "GAME OVER!!", centeredStyle);
		}
	}

	public void addPoints(string character, int pointVal){
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

