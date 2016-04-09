using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackgroundChanger : MonoBehaviour {

	
	// Background transition related variables
	public bool lightUpBG;
	public float lastAlpha;
	public GameObject darkBG;
	public int whichDeathLvl;
	public int whichNextLvl;
	public GameMaster GM;
    public Items items;
	public GameObject[] BGs;
	public List<GameObject> mushrooms;
	public Material diffuseMat;

    GameObject[] bgAnimList;

    // Use this for initialization
    void Start () {

		GameObject[] nightImg = GameObject.FindGameObjectsWithTag("NightMap");

		
		foreach (GameObject image in nightImg) {
			float alpha = image.GetComponent<Renderer>().material.color.a;
			//Color newColor = new Color(1, 1, 1, 0.0f);
			//image.GetComponent<SpriteRenderer>().material.color = newColor;
			Debug.Log ("Hiding night map");
		}

		//StartCoroutine(ActivateNight(240));
	}

	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.B)){

			goDark();


        }*/
        if (Input.GetKeyDown(KeyCode.T)){
			StartCoroutine(TrapFadeTo(1.0f, 0.25f));
		}
	}

	public void goDark(){
		//go to night map
		StartCoroutine(FadeTo(0.0f, 1.25f));
		
		//set GameMaster to darkmode
		//GM.isDark = true;
		
		//turn characters lights on
		foreach (Character character in GM.CHARACTERS) {
			if(character != null)
				character.activateLight();
		}

		foreach (GameObject mushroom in mushrooms) {
			
			Animator animator = mushroom.gameObject.GetComponent<Animator> ();
			animator.SetBool ("night", true);

			mushroom.gameObject.GetComponent<Renderer> ().material = diffuseMat;
		}
		
		//set items to night mode
		items.setDarkMode();
		
		//set background animations to night mode
		//bgAnimList = GameObject.FindGameObjectsWithTag("BackgroundAnim");
		/*foreach (GameObject bgAnim in bgAnimList)
		{
			if(bgAnim != null)
				bgAnim.gameObject.GetComponent<Animator>().SetBool("night", true);
		}*/
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

	IEnumerator ActivateNight(int time)
	{
		yield return new WaitForSeconds(time);

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
		
		//set background animations to night mode
		bgAnimList = GameObject.FindGameObjectsWithTag("BackgroundAnim");
		foreach (GameObject bgAnim in bgAnimList)
		{
			if(bgAnim != null)
				bgAnim.gameObject.GetComponent<Animator>().SetBool("night", true);
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