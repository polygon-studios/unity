using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InsideHouseMovement : MonoBehaviour {

	Character character;
	bool leftToRight;
	bool rightToLeft;
	bool leftToRightBindle;
	bool rightToLeftBinle;
	bool isInit;
    
    //moving and playing animations
    float savedItemDropperTimer = 3.2f;
	float itemDroppingTimer = 3.2f;
    bool itemDropAniPlaying = false;


	Vector3 targetRightHouse;
	Vector3 targetLeftHouse;
	Vector3 targetLeftItemDrop;
	Vector3 targetRightItemDrop;

	string itemName;

	Animator animator;

	// Use this for initialization
	void Start () {
	}

	public void initVariables(Character currChar, string side, string itemName1) {
		character = currChar;
		animator = character.GetComponent<Animator> ();
		itemName = itemName1;

		targetRightHouse = new Vector3 (16.5f, 0.45f, -6.0f);
		targetLeftHouse = new Vector3 (12.0f, 0.45f, -6.0f);
		targetLeftItemDrop = new Vector3 (14.0f, 0.45f, -6.0f);
		targetRightItemDrop = new Vector3 (15.0f, 0.45f, -6.0f);

		//updateAnimationPoints ();
		animator.SetBool ("insideHouse", true);

		if (side.Contains ("left") && itemName.Contains("none")) {
			leftToRight = true;
		} else if(side.Contains ("right") && itemName.Contains("none")){
			rightToLeft = true;
		}else if(side.Contains("left")){
			leftToRightBindle = true;
			animator.SetBool ("bindle", true);
			//playItemDrop (true, itemName);
		}
		isInit = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (isInit == true) { //check that all variables have been initialized prior to updating
			if (Input.GetKey (KeyCode.Z) || leftToRight == true) {
				character.transform.position = Vector3.MoveTowards (character.transform.position, targetRightHouse, Time.deltaTime * 1.5f);
				if (character.transform.position.x == targetRightHouse.x)
					resetAni ();
			} else if (Input.GetKey (KeyCode.X) || rightToLeft == true) {
				character.transform.position = Vector3.MoveTowards (character.transform.position, targetLeftHouse, Time.deltaTime * 1.5f);
				if (character.transform.position.x == targetLeftHouse.x)
					resetAni ();
			} else if (leftToRightBindle == true) {
				if(character.transform.position.x < targetLeftItemDrop.x){
					//leftToRightBINDLEPlaying = true;
					character.transform.position = Vector3.MoveTowards(character.transform.position, targetLeftItemDrop, Time.deltaTime*1.5f);
					animator.SetBool ("bindle", true);
	                //animator.SetBool(charID + "Bindle", true);
				}
				//if((float)Math.Round((double)character.transform.position.x,2) == targetLeftHouse.x){
				if(character.transform.position.x < targetLeftItemDrop.x + 0.1 && character.transform.position.x > targetLeftItemDrop.x - 0.1){
					animator.SetTrigger ("fistPump");
					animator.SetBool ("bindle", false);
	                //animator.SetBool(charID + "ItemDrop", true);
					itemDroppingTimer -= Time.deltaTime;

	                //play animation dropping item only once
	                /*if (itemDropAniPlaying == false)
	                {
	                    playItemDrop(true);
	                    itemDropAniPlaying = true;
	                }*/
				}
				if(itemDroppingTimer < 0){
					//itemDroppingTimer -= Time.deltaTime;
					//animator.SetBool(charID + "ItemDrop", false);
					character.transform.position = Vector3.MoveTowards(character.transform.position, targetRightHouse, Time.deltaTime*1.5f);
				}
				if(character.transform.position.x == targetRightHouse.x){
	                resetAni();
	              
				}
			}
		}
	}
                 
	/*void playItemDrop(bool isLeft)
    {
        Vector3 itemPos;
        if (isLeft == true)
            itemPos = new Vector3(-0.9f, -4.84f, 0);
        else {
            itemPos = new Vector3(1.3f, -4.84f, 0);
            
        }

		if (itemName.Contains("slippers"))
            currentItemObj = (GameObject)Instantiate(slippersPrefab, itemPos, Quaternion.identity);
		else if(itemName.Contains("chili"))
            currentItemObj = (GameObject)Instantiate(chiliPrefab, itemPos, Quaternion.identity);
		else if(itemName.Contains("treasure"))
            currentItemObj = (GameObject)Instantiate(treasurePrefab, itemPos, Quaternion.identity);
		else if(itemName.Contains("firework"))
            currentItemObj = (GameObject)Instantiate(fireworkPrefab, itemPos, Quaternion.identity);
		else if(itemName.Contains("ghost"))
            currentItemObj = (GameObject)Instantiate(ghostPrefab, itemPos, Quaternion.identity);
		else if(itemName.Contains("oil"))
            currentItemObj = (GameObject)Instantiate(oilPrefab, itemPos, Quaternion.identity);
		else if(itemName.Contains("pinwheel"))
            currentItemObj = (GameObject)Instantiate(pinwheelPrefab, itemPos, Quaternion.identity);
		else if (itemName.Contains("fish"))
            currentItemObj = (GameObject)Instantiate(fishPrefab, itemPos, Quaternion.identity);
        else 
            currentItemObj = (GameObject)Instantiate(prunePrefab, itemPos, Quaternion.identity);
        
        if(isLeft == true)
            currentItemObj.transform.eulerAngles = new Vector2(0, 0);
        else
            currentItemObj.transform.eulerAngles = new Vector2(0, 180);
    }*/


	void resetAni(){
		animator.SetBool ("insideHouse", false);
		character.isStunned = false;
		character.isInHouse = false;
		Destroy (this.gameObject);
		itemDroppingTimer = savedItemDropperTimer;

		/*if(currentItemObj != null)
		{
			Destroy(currentItemObj);
		}*/

	}
    
    

	/*void updateAnimationPoints(){
		leftToRightPoints = new List<Vector3> ();
		leftToRightPoints.Add (new Vector3 (-8f, -6.3f, 0f));
		leftToRightPoints.Add (new Vector3 (8.5f, -6.3f, 0f));

		leftToRightBINDLEPoints = new List<Vector3> ();
		leftToRightBINDLEPoints.Add(new Vector3 (-8f, -6.3f, 0f));
		leftToRightBINDLEPoints.Add(new Vector3 (-1.6f, -6.3f, 0f));
		leftToRightBINDLEPoints.Add (new Vector3 (8.5f, -6.3f, 0f));

		rightToLeftBINDLEPoints = new List<Vector3>();
		rightToLeftBINDLEPoints.Add(new Vector3(8.5f, -6.3f, 0f));
		rightToLeftBINDLEPoints.Add(new Vector3(2.1f, -6.3f, 0f));
		rightToLeftBINDLEPoints.Add(new Vector3(-8f, -6.3f, 0f));
	}*/


}
