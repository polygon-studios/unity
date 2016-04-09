﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	Text txt;
	private int currentScore;
	public bool isHidden = false;
	public string customText;

	// Use this for initialization
	void Start () {
		currentScore = 0;
		txt = gameObject.GetComponent<Text>(); 
		txt.text= "" + currentScore;
	}
	
	// Update is called once per frame
	void Update () {
		if (customText != null) {
			
		} else {
			txt.text = "" + currentScore;
		}
		if (isHidden) {
			txt.text= " ";
		}

	}

	public void updateScore(int points){
		currentScore = points;
	}

	public void addCustomText(string text){
		customText = text;
	}




}
