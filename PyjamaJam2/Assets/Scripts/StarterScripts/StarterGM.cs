using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarterGM : MonoBehaviour {

    public GameObject splashScreen;
    public List<GameObject> possibleCharacters;
    public List<bool> isSelectedCharacter;
    //public List<GameObject> savedCharacterOrder;

    public string playerControllerName = "Player";
    public string buttonStart = "_start";

    public bool passedStartScreen = false;

    int totalSelectedChars = 0;

    List<Vector2> controllerToCharacter = new List<Vector2> (); //fox 1, skunk 2, rabbit 2, bear 4

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetButtonDown(playerControllerName + "1" + buttonStart) ||
            Input.GetButtonDown(playerControllerName + "2" + buttonStart) ||
            Input.GetButtonDown(playerControllerName + "3" + buttonStart) ||
            Input.GetButtonDown(playerControllerName + "4" + buttonStart)) && passedStartScreen == false)
        {
            StartCoroutine(FadeTo(0.0f, 1.0f, splashScreen));
            passedStartScreen = true;
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
        controllerToCharacter.Add(new Vector2(controllerNum, characterNum));
        isSelectedCharacter[characterNum] = true;
        totalSelectedChars++;
    }

    public void updateScreen(int characterNum)
    {
        possibleCharacters[characterNum].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }
}
