using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster:MonoBehaviour
{
	public static GameMaster GM;
	public List<Character> CHARACTERS;
	public bool isDark;

    private bool showGUI = true;

	public GameObject foxCanvas;
	public GameObject skunkCanvas;
	public GameObject bearCanvas;
	public GameObject rabbitCanvas;

	ScoreText foxText;
	ScoreText skunkText;
	ScoreText bearText;
	ScoreText rabbitText;

    int foxScore;
	int skunkScore;
	int bearScore;
	int rabbitScore;
	static float timer;
	bool gameOver;

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

		foxText = foxCanvas.GetComponent<ScoreText> ();
		skunkText = skunkCanvas.GetComponent<ScoreText> ();
		bearText = bearCanvas.GetComponent<ScoreText> ();
		rabbitText = rabbitCanvas.GetComponent<ScoreText> ();
	}

	void Update(){

		timer += Time.deltaTime;

	}


	public void addPoints(string character, int pointVal){
		Debug.Log ("ADDING SCORE");
		if (character.Contains ("Fox")) {
			foxScore += foxScore + pointVal;
			foxText.updateScore (foxScore);
		}
		if (character.Contains ("Skunk")) {
			skunkScore += skunkScore + pointVal;
			skunkText.updateScore (skunkScore);
		}
		if (character.Contains ("Bear")) {
			bearScore += bearScore + pointVal;
			bearText.updateScore (bearScore);
		}
		if (character.Contains ("Rabbit")) {
			rabbitScore += rabbitScore + pointVal;
			rabbitText.updateScore (rabbitScore);
		}
	}


}

