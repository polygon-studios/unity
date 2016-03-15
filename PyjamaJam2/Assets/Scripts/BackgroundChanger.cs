using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundChanger : MonoBehaviour {

	
	// Background transition related variables
	public bool lightUpBG;
	public float lastAlpha;
	public GameObject darkBG;
	public int whichDeathLvl;
	public int whichNextLvl;
	public GameMaster GM;
    public Items items;
	
	
	
	// Use this for initialization
	void Start () {
		GameObject[] trap = GameObject.FindGameObjectsWithTag("Trap");
		
		foreach (GameObject traps in trap) {
			float alpha = traps.GetComponent<Renderer>().material.color.a;
			
			Color newColor = new Color(1, 1, 1, 0.0f);
			traps.GetComponent<Renderer>().material.color = newColor;
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)){

			//go to night map
			StartCoroutine(FadeTo(0.0f, 3.75f));

			//set GameMaster to darkmode
			GM.isDark = true;

			//turn characters lights on
			foreach (Character character in GM.CHARACTERS) {
                if(character != null)
				    character.activateLight();
			}

            //set items to night mode
            items.setDarkMode();
		}
		if(Input.GetKeyDown(KeyCode.T)){
			StartCoroutine(TrapFadeTo(1.0f, 0.25f));
		}
	}
	
	

	IEnumerator FadeTo(float aValue, float aTime)
	{
		GameObject[] BGs = GameObject.FindGameObjectsWithTag("DayMap");
		
		foreach (GameObject background in BGs) {
			float alpha = background.GetComponent<Renderer>().material.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
				background.GetComponent<Renderer>().material.color = newColor;
				yield return null;
			}
		}
	}

	IEnumerator TrapFadeTo(float aValue, float aTime)
	{
		GameObject[] BGs = GameObject.FindGameObjectsWithTag("Trap");
		
		foreach (GameObject background in BGs) {
			float alpha = background.GetComponent<Renderer>().material.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
				background.GetComponent<Renderer>().material.color = newColor;
				yield return null;
			}
		}
	}

}