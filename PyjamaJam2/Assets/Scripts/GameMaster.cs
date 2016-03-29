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

	string first;
	string second;
	string third;
	string fourth;

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

		if (foxScore > skunkScore && foxScore > bearScore && foxScore > rabbitScore) {
			first = "fox";
			if (skunkScore > bearScore && skunkScore > rabbitScore) {
				second = "skunk";
				if(rabbitScore > bearScore){
					third = "rabbit";
					fourth = "bear";
				}
				else {
					third = "bear";
					fourth = "rabbit";
				}
			}
			if (bearScore > skunkScore && bearScore > rabbitScore) {
				second = "bear";
				if(rabbitScore > skunkScore){
					third = "rabbit";
					fourth = "skunk";
				}
				else {
					third = "skunk";
					fourth = "rabbit";
				}
			}
			if (rabbitScore > bearScore && rabbitScore > skunkScore) {
				second = "rabbit";
				if(bearScore > skunkScore){
					third = "bear";
					fourth = "skunk";
				}
				else {
					third = "skunk";
					fourth = "bear";
				}
			}
		}
		if (skunkScore > foxScore && skunkScore > bearScore && skunkScore > rabbitScore) {
			first = "skunk";
			if (foxScore > bearScore && foxScore > rabbitScore) {
				second = "fox";
				if(rabbitScore > bearScore){
					third = "rabbit";
					fourth = "bear";
				}
				else {
					third = "bear";
					fourth = "rabbit";
				}
			}
			if (bearScore > foxScore && bearScore > rabbitScore) {
				second = "bear";
				if(foxScore > rabbitScore){
					third = "fox";
					fourth = "rabbit";
				}
				else {
					third = "rabbit";
					fourth = "fox";
				}
			}
			if (rabbitScore > bearScore && rabbitScore > foxScore) {
				second = "rabbit";
				if(foxScore > bearScore){
					third = "fox";
					fourth = "bear";
				}
				else {
					third = "bear";
					fourth = "fox";
				}
			}
		}
		if (bearScore > skunkScore && bearScore > foxScore && bearScore > rabbitScore) {
			first = "bear";
			if (skunkScore > foxScore && skunkScore > rabbitScore) {
				second = "skunk";
				if(foxScore > rabbitScore){
					third = "fox";
					fourth = "rabbit";
				}
				else {
					third = "rabbit";
					fourth = "fox";
				}
			}
			if (foxScore > skunkScore && foxScore > rabbitScore) {
				second = "fox";
				if(rabbitScore > skunkScore){
					third = "rabbit";
					fourth = "skunk";
				}
				else {
					third = "skunk";
					fourth = "rabbit";
				}
			}
			if (rabbitScore > foxScore && rabbitScore > skunkScore) {
				second = "rabbit";
				if(foxScore > skunkScore){
					third = "fox";
					fourth = "skunk";
				}
				else {
					third = "skunk";
					fourth = "fox";
				}
			}
		}
		if (rabbitScore > skunkScore && rabbitScore > bearScore && rabbitScore > foxScore) {
			first = "rabbit";
			if (skunkScore > bearScore && skunkScore > foxScore) {
				second = "skunk";
				if(foxScore > bearScore){
					third = "fox";
					fourth = "bear";
				}
				else {
					third = "bear";
					fourth = "fox";
				}
			}
			if (bearScore > skunkScore && bearScore > foxScore) {
				second = "bear";
				if(foxScore > skunkScore){
					third = "fox";
					fourth = "skunk";
				}
				else {
					third = "skunk";
					fourth = "fox";
				}
			}
			if (foxScore > bearScore && foxScore > skunkScore) {
				second = "fox";
				if(bearScore > skunkScore){
					third = "bear";
					fourth = "skunk";
				}
				else {
					third = "skunk";
					fourth = "bear";
				}
			}
		}

	}


	public void addPoints(string character, int pointVal){
		Debug.Log ("ADDING SCORE");
		if (character.Contains ("Fox")) {
			foxScore = foxScore + pointVal;
			foxText.updateScore (foxScore);
			Debug.Log("Fox score: " + foxScore);
		}
		if (character.Contains ("Skunk")) {
			skunkScore = skunkScore + pointVal;
			skunkText.updateScore (skunkScore);
			Debug.Log("Skunk score: " + skunkScore);
		}
		if (character.Contains ("Bear")) {
			bearScore = bearScore + pointVal;
			bearText.updateScore (bearScore);
			Debug.Log("Bear score: " + bearScore);
		}
		if (character.Contains ("Rabbit")) {
			rabbitScore = rabbitScore + pointVal;
			rabbitText.updateScore (rabbitScore);
			Debug.Log("Rabbit score: " + rabbitScore);
		}
	}


}

