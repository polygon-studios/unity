using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class StarterGM : MonoBehaviour {

    public GameObject splashScreen;
	public GameObject pressStartCharSel;
	public GameObject instructions;
	public GameObject dontDestroy;
	public Text charSelTimerText;
    public List<GameObject> possibleCharacters;
	public List<GameObject> charSelObjs;
    public List<bool> isSelectedCharacter;
    //public List<GameObject> savedCharacterOrder;

    public string playerControllerName = "Player";
    public string buttonStart = "_start";

    public bool passedStartScreen = false;
	public bool passedCharacterSelScreen = false;

    int totalSelectedChars = 0;
	float charSelTimer = 20f;

    //public List<Vector2> controllerToCharacter = new List<Vector2> (); //fox 1, skunk 2, rabbit 2, bear 3


	// Use this for initialization
	void Start () {
		pressStartCharSel.gameObject.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, 0f);
	}
	
	// Update is called once per frame
	void Update () {

		if ((Input.GetButtonDown (playerControllerName + "1" + buttonStart) ||
		          Input.GetButtonDown (playerControllerName + "2" + buttonStart) ||
		          Input.GetButtonDown (playerControllerName + "3" + buttonStart) ||
		          Input.GetButtonDown (playerControllerName + "4" + buttonStart)) && passedStartScreen == false) {
			StartCoroutine (FadeTo (0.0f, 0.5f, splashScreen));
			passedStartScreen = true;
		}

		if (passedStartScreen == true && passedCharacterSelScreen == false){
			if (totalSelectedChars < 4) {
				charSelTimer -= Time.deltaTime;
				charSelTimerText.text = "" + Mathf.RoundToInt (charSelTimer);
			}

			if (charSelTimer < 0 || totalSelectedChars == 4) {
				StartCoroutine (FadeTo (0.0f, 0.5f, pressStartCharSel.gameObject));
				passedCharacterSelScreen = true;

				//hide everything involved with character selection
				foreach (GameObject obj in charSelObjs) {
					StartCoroutine (FadeTo (0.0f, 0.5f, obj));
				}
				charSelTimerText.text = "";
				SceneManager.LoadScene (1);
			}
		}
		if (passedCharacterSelScreen == true) {
			Debug.Log ("show instructions");
		}
    }
    

    IEnumerator FadeTo(float aValue, float aTime, GameObject go)
    {
        float alpha = go.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
			go.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }

    public void setCharToController(int characterNum, int controllerNum)
    {
		dontDestroy.GetComponent<DontDestroy>().controllerToCharacter.Add(new Vector2(controllerNum, characterNum));
        isSelectedCharacter[characterNum] = true;
        totalSelectedChars++;
    }

    public void updateScreen(int characterNum)
    {
        possibleCharacters[characterNum].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }

	public void removeSelection(int characterNum){
		possibleCharacters[characterNum].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		totalSelectedChars--;
		isSelectedCharacter[characterNum] = false;

		foreach (Vector2 contToChar in dontDestroy.GetComponent<DontDestroy>().controllerToCharacter) {
			if (contToChar.y == characterNum) {
				dontDestroy.GetComponent<DontDestroy>().controllerToCharacter.Remove (contToChar);
				break;
			}
		}
	}
}
