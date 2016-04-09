using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster:MonoBehaviour
{
	public static GameMaster GM;
	public List<Character> CHARACTERS;
	public bool isDark;

    private bool showGUI = true;

	GameObject controllerToChar;

	public GameObject foxCanvas;
	public GameObject skunkCanvas;
	public GameObject bearCanvas;
	public GameObject rabbitCanvas;
	public GameObject countDownTimerCanvas;
	public BackgroundChanger backgroundChanger;
	public GameObject sleepSongPrefab;
	GameObject sleepSongRef;

	public AudioClip audioSleepSong;
	bool audioSleepSongPlaying;

	public SocketIOLogic io;

	float nightTimer = 240f;
	float fullGameTimer = 480f;
	float restartGameTimer = 500f;

	public string first;
	public string second;
	public string third;
	public string fourth;

	public int foxScore;
	public int skunkScore;
	public int bearScore;
	public int rabbitScore;

	public int foxButtons;
	public int skunkButtons;
	public int bearButtons;
	public int rabbitButtons;

	public int trapsPlaced;
	public int itemsCollected;

	ScoreBoardText foxText;
	ScoreBoardText skunkText;
	ScoreBoardText bearText;
	ScoreBoardText rabbitText;
	ScoreText countDownText;

	public GameObject foxScoreboard;
	public GameObject skunkScoreboard;
	public GameObject bearScoreboard;
	public GameObject rabbitScoreboard;
	
	bool gameOver;
	bool sunSet;
	bool darknessSet;
	bool darknessFaded;

	float countDownTimerVal = 20f;

	void Awake(){
		if (GM != null)
			GameObject.Destroy (GM);
		else
			GM = this;
		DontDestroyOnLoad (this);

	}

	void OnEnable(){
		GameObject[] charGO = GameObject.FindGameObjectsWithTag ("character");
		foreach (GameObject GO in charGO) {
			CHARACTERS.Add(GO.GetComponent<Character>());
		}

		controllerToChar = GameObject.Find ("DontDestroy");
		if(controllerToChar != null)
			setCharactersAndControllers (controllerToChar.GetComponent<DontDestroy> ().controllerToCharacter);


		GameObject nightImg = GameObject.Find ("BlackScreen");
		
		if (nightImg) {
			float alpha = nightImg.GetComponent<Renderer>().material.color.a;
			Color newColor = new Color(1, 1, 1, 0.0f);
			nightImg.GetComponent<SpriteRenderer>().material.color = newColor;
			Debug.Log ("Hiding black map");
		}

		Color hiddenColor = new Color(1, 1, 1, 0.0f);
		Color showColor = new Color(1, 1, 1, 1.0f);

		foxText = foxCanvas.GetComponent<ScoreBoardText>();
		skunkText = skunkCanvas.GetComponent<ScoreBoardText>();
		bearText = bearCanvas.GetComponent<ScoreBoardText>();
		rabbitText = rabbitCanvas.GetComponent<ScoreBoardText>();

		foxScoreboard.GetComponent<SpriteRenderer>().material.color = hiddenColor;
		skunkScoreboard.GetComponent<SpriteRenderer>().material.color = hiddenColor;
		bearScoreboard.GetComponent<SpriteRenderer>().material.color = hiddenColor;
		rabbitScoreboard.GetComponent<SpriteRenderer>().material.color = hiddenColor;
	

		Debug.Log ("Hiding black map");



		foreach (Character player in CHARACTERS){
			if(player.name.Contains("Fox")){
				foxText.isHidden = false;
				foxScoreboard.GetComponent<SpriteRenderer>().material.color = showColor;
			}
			if(player.name.Contains("Skunk")){
				skunkText.isHidden = false;
				skunkScoreboard.GetComponent<SpriteRenderer>().material.color = showColor;
			}
			if(player.name.Contains("Bear")){
				bearText.isHidden = false;
				bearScoreboard.GetComponent<SpriteRenderer>().material.color = showColor;
			}
			if(player.name.Contains("Rabbit")){
				rabbitText.isHidden = false;
				rabbitScoreboard.GetComponent<SpriteRenderer>().material.color = showColor;
			}
		}

	}

	void Start(){
		foxScore = 0;
		skunkScore = 0;
		bearScore = 0;
		rabbitScore = 0;

		foxButtons = 0;
		skunkButtons = 0;
		bearButtons = 0;
		rabbitButtons = 0;

		first = "Fox";
		second = "Skunk";
		third = "Bear";
		fourth = "Rabbit";

		countDownText = countDownTimerCanvas.GetComponent<ScoreText> ();

		countDownText.isHidden = true;

		GameObject sunsetImg = GameObject.FindGameObjectWithTag("SunsetFilter");
		
		if (sunsetImg) {
			float alpha = sunsetImg.GetComponent<Renderer>().material.color.a;
			Color newColor = new Color(1, 1, 1, 0.0f);
			sunsetImg.GetComponent<SpriteRenderer>().material.color = newColor;
			Debug.Log ("Hiding sunset map");
		}

		sunSet = false;
		darknessSet = false;
		darknessFaded = false;
	}



	void Update(){


		if (isDark == false) {
			nightTimer -= Time.deltaTime;
		}

		if (Input.GetKeyDown ("space")) {
			//io.endGame (first, second, third, fourth);
			Debug.Log ("FoxText: " + foxText);
			//Debug.Log ("TRYING TO SEND ENDGAME");

		}

		if (nightTimer < 0 || Input.GetKey(KeyCode.B)) {

			backgroundChanger.goDark();
			isDark = true;
		}

		fullGameTimer -= Time.deltaTime;
		restartGameTimer -= Time.deltaTime;

		if(nightTimer < 20f && !sunSet){
			StartCoroutine(FadeTo(0.3f, 15.00f, "SunsetFilter"));
			sunSet = true;
		}
		if(nightTimer < 10f && !darknessSet){
			StartCoroutine(FadeTo(0.0f, 10.00f, "SunsetFilter"));
			StartCoroutine(FadeTo(0.4f, 8.00f, "BlackScreen"));
			darknessSet = true;
		}
		//Debug.Log (fullGameTimer);
		if (fullGameTimer < 220f && !darknessFaded) {
			StartCoroutine(FadeTo(0.0f, 10.00f, "BlackScreen"));
			darknessFaded = true;
		}

		if (fullGameTimer < 0) {
			Debug.Log("END GAME END GAME");
			countDownText.isHidden = false;
			countDownTimerVal -= Time.deltaTime;
			int countDownInt = (int)countDownTimerVal;
			countDownText.updateScore (countDownInt);
			StartCoroutine(FadeTo(1.0f, 4.75f, "BlackScreen"));
		}
		if (restartGameTimer < 0) {
			countDownText.addCustomText("Restart?");
		}


		if (countDownTimerVal < 0) {

			if (audioSleepSongPlaying == false) {
				sleepSongRef = (GameObject)Instantiate (sleepSongPrefab, new Vector3 (15, 0f, 0f), Quaternion.identity);
				audioSleepSongPlaying = true;
			}

			io.endGame(first, second, third, fourth);
			countDownText.isHidden = true;
		}
	}

	IEnumerator FadeTo(float aValue, float aTime, string tag)
	{
		GameObject nightImg = GameObject.Find (tag);
		Debug.Log ("Fading the " + tag + " tag to a value of " + aValue);
		
		if (nightImg) {
			float alpha = nightImg.GetComponent<Renderer>().material.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
				nightImg.GetComponent<Renderer>().material.color = newColor;
				yield return null;
			}
		}
	}

	void setCharactersAndControllers(List<Vector2> contToChars){
		if (contToChars != null) {
			foreach (Character character in CHARACTERS) {
				bool foundChar = false;
				foreach (Vector2 contToChar in contToChars) {
					if (contToChar.x != 0) {
						if (character.charID.Contains ("fox") && contToChar.y == 0) {
							foundChar = true;
							character.controllerNumber = (int)contToChar.x;
						} else if (character.charID.Contains ("skunk") && contToChar.y == 1) {
							foundChar = true;
							character.controllerNumber = (int)contToChar.x;
						} else if (character.charID.Contains ("rabbit") && contToChar.y == 2) {
							foundChar = true;
							character.controllerNumber = (int)contToChar.x;
						} else if (character.charID.Contains ("bear") && contToChar.y == 3) {
							foundChar = true;
							character.controllerNumber = (int)contToChar.x;
						}
					}			


				}
				if (foundChar == false) {
					character.destroySelf ();
					character.controllerNumber = 0;
					//remove this character from gameplay
					//remove character from stroed list
				}
			}

			List<Character> newTempCharList = new List<Character> ();
			foreach (Character character in CHARACTERS) {
				if (character.controllerNumber > 0)
					newTempCharList.Add (character);
			}

			CHARACTERS = new List<Character> ();
			CHARACTERS = newTempCharList;
		}
	}

	public void addPoints(string character, int pointVal){
		Debug.Log ("ADDING SCORE");
		if (character.Contains ("Fox")) {
			foxScore = foxScore + pointVal;
			Debug.Log ("FoxText: " + foxText);
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

		first = "Fox";
		second = "Skunk";
		third = "Bear";
		fourth = "Rabbit";

		if (foxScore > skunkScore && foxScore > bearScore && foxScore > rabbitScore) {
			first = "Fox";
			if (skunkScore >= bearScore && skunkScore >= rabbitScore) {
				second = "Skunk";
				if(rabbitScore >= bearScore){
					third = "Rabbit";
					fourth = "Bear";
				}
				else {
					third = "Bear";
					fourth = "Rabbit";
				}
			}
			if (bearScore >= skunkScore && bearScore >= rabbitScore) {
				second = "Bear";
				if(rabbitScore >= skunkScore){
					third = "Rabbit";
					fourth = "Skunk";
				}
				else {
					third = "Skunk";
					fourth = "Rabbit";
				}
			}
			if (rabbitScore >= bearScore && rabbitScore >= skunkScore) {
				second = "Rabbit";
				if(bearScore >= skunkScore){
					third = "Bear";
					fourth = "Skunk";
				}
				else {
					third = "Skunk";
					fourth = "Bear";
				}
			}
		}
		if (skunkScore > foxScore && skunkScore > bearScore && skunkScore > rabbitScore) {
			first = "Skunk";
			if (foxScore >= bearScore && foxScore >= rabbitScore) {
				second = "fox";
				if(rabbitScore >= bearScore){
					third = "Rabbit";
					fourth = "Bear";
				}
				else {
					third = "Bear";
					fourth = "Rabbit";
				}
			}
			if (bearScore >= foxScore && bearScore >= rabbitScore) {
				second = "Bear";
				if(foxScore >= rabbitScore){
					third = "fox";
					fourth = "Rabbit";
				}
				else {
					third = "Rabbit";
					fourth = "fox";
				}
			}
			if (rabbitScore >= bearScore && rabbitScore >= foxScore) {
				second = "Rabbit";
				if(foxScore >= bearScore){
					third = "fox";
					fourth = "Bear";
				}
				else {
					third = "Bear";
					fourth = "fox";
				}
			}
		}
		if (bearScore > skunkScore && bearScore > foxScore && bearScore > rabbitScore) {
			first = "Bear";
			if (skunkScore >= foxScore && skunkScore >= rabbitScore) {
				second = "Skunk";
				if(foxScore >= rabbitScore){
					third = "fox";
					fourth = "rabbit";
				}
				else {
					third = "rabbit";
					fourth = "fox";
				}
			}
			if (foxScore >= skunkScore && foxScore >= rabbitScore) {
				second = "fox";
				if(rabbitScore >= skunkScore){
					third = "Rabbit";
					fourth = "Skunk";
				}
				else {
					third = "Skunk";
					fourth = "Rabbit";
				}
			}
			if (rabbitScore >= foxScore && rabbitScore >= skunkScore) {
				second = "Rabbit";
				if(foxScore >= skunkScore){
					third = "fox";
					fourth = "Skunk";
				}
				else {
					third = "Skunk";
					fourth = "fox";
				}
			}
		}
		if (rabbitScore > skunkScore && rabbitScore > bearScore && rabbitScore > foxScore) {
			first = "Rabbit";
			if (skunkScore >= bearScore && skunkScore >= foxScore) {
				second = "Skunk";
				if(foxScore >= bearScore){
					third = "fox";
					fourth = "Bear";
				}
				else {
					third = "Bear";
					fourth = "fox";
				}
			}
			if (bearScore >= skunkScore && bearScore >= foxScore) {
				second = "Bear";
				if(foxScore >= skunkScore){
					third = "fox";
					fourth = "Skunk";
				}
				else {
					third = "Skunk";
					fourth = "fox";
				}
			}
			if (foxScore >= bearScore && foxScore >= skunkScore) {
				second = "fox";
				if(bearScore >= skunkScore){
					third = "Bear";
					fourth = "Skunk";
				}
				else {
					third = "Skunk";
					fourth = "Bear";
				}
			}
		}
	}

	//see how the character ranks against other characters for score
	public int getCurrentRank(string charName){
		
		if (first.Contains (charName)) {
			return 1;
		} else if (second.Contains (charName)) {
			return 2;
		} else if (third.Contains (charName)) {
			return 3;
		} else if (fourth.Contains (charName)) {
			return 4;
		} else
			return 0;

	}

	public void addItemPickup(string character) {
		itemsCollected++;
	}

	public void buttonsMashed(string character) {
		if (character.Contains ("Fox")) {
			foxButtons++;
		}
		if (character.Contains ("Skunk")) {
			skunkButtons++;
		}
		if (character.Contains ("Bear")) {
			bearButtons++;
		}
		if (character.Contains ("Rabbit")) {
			rabbitButtons++;
		}
	}

	public void addTrapPickup() {
		trapsPlaced++;
	}
}

