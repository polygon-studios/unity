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
	
	
	
	// Use this for initialization
	void Start () {
		GameObject[] BGs = GameObject.FindGameObjectsWithTag("NightMap");
		
		foreach (GameObject background in BGs) {
			float alpha = background.GetComponent<Renderer>().material.color.a;

			Color newColor = new Color(1, 1, 1, 0.0f);
			background.GetComponent<Renderer>().material.color = newColor;
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)){
			StartCoroutine(FadeTo(1.0f, 3.75f));
		}
	}
	
	

	IEnumerator FadeTo(float aValue, float aTime)
	{
		GameObject[] BGs = GameObject.FindGameObjectsWithTag("NightMap");
		
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