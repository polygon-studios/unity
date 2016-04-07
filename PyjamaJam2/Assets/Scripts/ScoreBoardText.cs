using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoardText : MonoBehaviour {

	Text txt;
	private int currentScore;

	// Use this for initialization
	void Start () {
		currentScore = 0;
		txt = gameObject.GetComponent<Text>(); 
		txt.text= "" + currentScore;
	}

	// Update is called once per frame
	void Update () {
		txt.text= "" + currentScore; 
	}

	public void updateScore(int points){
		currentScore = points;
	}

}
