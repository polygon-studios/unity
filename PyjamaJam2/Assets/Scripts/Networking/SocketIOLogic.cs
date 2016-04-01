using UnityEngine;
using SocketIO;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class SocketIOLogic : MonoBehaviour
{
	private SocketIOComponent socket;
	public GameMaster GM;
	public GameObject buttonPrefab;
	public GameObject bramblePrefab;
	public GameObject pineconePrefab;
	bool isCalled = false;

	public void Start()
	{
		GameObject go = GameObject.FindWithTag("SocketIO");
		if (go) {
			socket = go.GetComponent<SocketIOComponent> ();
		}
		socket.On("open", TestOpen);
		socket.On("news", TestBoop);
		socket.On("success", TestBoop);
		socket.On("trapPlace", placeTrap);
		socket.On("boop", TestBoop);
		socket.On("error", TestError);
		socket.On("close", TestClose);
		socket.On("playerEnter", fuckYou);

		StartCoroutine(BeepBoop(1.0f));
        InvokeRepeating("getPlayerPositions", 0.5f, 0.3f);
    }

	public void Update()
	{
		//getPlayerPositions ();
		if (GM.isDark == true && !isCalled) {
			socket.Emit ("nighttime");
			isCalled = true;
		}
    }

	IEnumerator BeepBoop(float disValue)
	{
		Debug.Log ("Testing beepboop");

		// wait 3 seconds and continue
		yield return new WaitForSeconds(3);

		socket.Emit("beep");
		// wait 2 seconds and continue
		yield return new WaitForSeconds(2);

		socket.Emit("getPositions");
	}


	public void TestBoop(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

		if (e.data == null) { return; }

		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
			);
	}

	public void placeTrap(SocketIOEvent e)
	{
		Debug.Log("Trap x pos:" + e.data["pos-x"] + " trap y pos: " + e.data["pos-y"]);


		string tempx = string.Format ("{0}", e.data ["pos-x"]);
		float xPos = (float.Parse (tempx)) * 0.733f;
		string tempy = string.Format ("{0}", e.data ["pos-y"]);
		float yPos = (float.Parse (tempy)) * 0.747f;
		string tempTrap = string.Format ("{0}", e.data ["trap"]);

		Debug.Log(tempTrap);

		if (tempTrap.Contains ("bramble")) {
			GameObject trap = (GameObject)Instantiate (bramblePrefab, new Vector3 (xPos, yPos, -7), Quaternion.identity);
			Debug.Log("Placing bramble");
		} else if (tempTrap.Contains("pinecone")) {
			GameObject pinecone = (GameObject)Instantiate (pineconePrefab, new Vector3 (xPos, yPos, -7), Quaternion.identity);
		}
        else {
            GameObject button = (GameObject)Instantiate(buttonPrefab, new Vector3(xPos, yPos, -7), Quaternion.identity);
        }
		if (e.data == null) { return; }

		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
			);
	}

	/*
	 *
	 * Open, close and error handling functions
	 *
	 */

	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}

	public void TestClose(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}

	/*
	 *
	 * Non- socket.io functions
	 *
	 */
	public void getPlayerPositions() {
		GameObject[] Players = GameObject.FindGameObjectsWithTag("character");
		Dictionary<string, string> data = new Dictionary<string, string>();

		foreach (GameObject character in Players) {
			float positionX = character.transform.position.x;
			float positionY = character.transform.position.y;

			if(character.name == "character_Fox"){
				data["foxX"] = positionX.ToString();
				data["foxY"] = positionY.ToString();
			}
			if(character.name == "character_Skunk"){
				data["skunkX"] = positionX.ToString();
				data["skunkY"] = positionY.ToString();
			}
			if(character.name == "character_Bear"){
				data["bearX"] = positionX.ToString();
				data["bearY"] = positionY.ToString();
			}
			if(character.name == "character_Rabbit"){
				data["rabbitX"] = positionX.ToString();
				data["rabbitY"] = positionY.ToString();
			}

		}
		socket.Emit("playerPositions", new JSONObject(data));

	}

	public void playerEnter(string name, string side, string item) {
		Dictionary<string, string> data = new Dictionary<string, string>();

		data["character"] = name;
		data["side"] = side;
		data["holdingItem"] = item;

		socket.Emit("playerEnter", new JSONObject(data));
	}

	public void lockHouse() {
		
		socket.Emit("redButton");
	}

	public void endGame(string firstPlace, string secondPlace, string thirdPlace, string fourthPlace) {

		Dictionary<string, string> data = new Dictionary<string, string>();
		data["first"] = firstPlace;
		data["second"] = secondPlace;
		data["third"] = thirdPlace;
		data["fourth"] = fourthPlace;
		
		socket.Emit("endGame", new JSONObject(data));
		Debug.Log ("Sent the endgame");
	}


	public void fuckYou (SocketIOEvent e){
		Debug.Log("Character " + e.data["character"] + " came from " + e.data["side"] + " with ze item " + e.data["holdingItem"]);


		string tempSide = string.Format ("{0}", e.data ["side"]);
		float side = (float.Parse (tempSide)) * 1.0f;

		string tempChar = string.Format ("{0}", e.data ["character"]);
		string tempItem = string.Format ("{0}", e.data ["holdingItem"]);

		Debug.Log("Character " + tempChar + " came from " + side + " with ze item " + tempItem);
	}

}
