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
	public GameObject TrapMaster;
	public GameObject buttonPrefab;
	public GameObject bramblePrefab;
	public GameObject pineconePrefab;
	bool isCalled = false;

	int trapsPlaced;
	int itemsCollected;

	TrapMaster TM;

	public void Start()
	{
		GameObject go = GameObject.FindWithTag("SocketIO");
		if (go) {
			socket = go.GetComponent<SocketIOComponent> ();
		}

		trapsPlaced = 0;
		itemsCollected = 0;

		TM = TrapMaster.GetComponent<TrapMaster> ();

		socket.On("open", TestOpen);
		socket.On("news", TestBoop);
		socket.On("success", TestBoop);
		socket.On("trapPlace", placeTrap);
		socket.On("marco", sayPolo);
		socket.On("boop", TestBoop);
		socket.On("error", TestError);
		socket.On("close", TestClose);

        InvokeRepeating("getPlayerPositions", 0.5f, 0.3f);
		InvokeRepeating("getDashboardStats", 1.0f, 1.0f);
    }

	public void Update()
	{
		//getPlayerPositions ();
		if (GM.isDark == true && !isCalled) {
			socket.Emit ("nighttime");
			isCalled = true;
		}
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
		string tempx = string.Format ("{0}", e.data ["pos-x"]);
		float xPos = (float.Parse (tempx)) * 0.722f + 0.5f;
		Debug.Log("Retreived xPos: " + xPos);
		string tempy = string.Format ("{0}", e.data ["pos-y"]);
		float yPos = (float.Parse (tempy)) * 0.747f;
		Debug.Log("Retreived yPos: " + yPos);

		string tempID = string.Format ("{00}", e.data ["ID"]);

		Debug.Log("String ID: " + tempID);

		string tempTrap = string.Format ("{0}", e.data ["trap"]);


		TM.generateTrap (tempTrap, xPos, yPos, tempID);

		if (e.data == null) { return; }

		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
			);
	}

	public void sayPolo(SocketIOEvent e)
	{
		socket.Emit("polo");
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

	public void getDashboardStats() {
		Dictionary<string, string> data = new Dictionary<string, string>();
		string firstPlace = GM.first.ToString();
		string secondPlace = GM.second.ToString();
		string thirdPlace = GM.third.ToString();
		string fourthPlace = GM.fourth.ToString();

		string foxScore = GM.foxScore.ToString ();
		string skunkScore = GM.skunkScore.ToString();
		string bearScore = GM.bearScore.ToString ();
		string rabbitScore = GM.rabbitScore.ToString();

		string foxButtons = GM.foxButtons.ToString ();
		string skunkButtons = GM.skunkButtons.ToString();
		string bearButtons = GM.bearButtons.ToString ();
		string rabbitButtons = GM.rabbitButtons.ToString();

		data ["firstChar"] = firstPlace;
		if (firstPlace.Contains ("Fox")) {
			data ["firstPoints"] = foxScore;
			data ["firstButtons"] = foxButtons;
		}
		if(firstPlace.Contains ("Skunk")) {
			data ["firstPoints"] = skunkScore;
			data ["firstButtons"] = skunkButtons;
		}
		if(firstPlace.Contains ("Bear")) {
			data ["firstPoints"] = bearScore;
			data ["firstButtons"] = bearButtons;
		}
		if(firstPlace.Contains ("Rabbit")) {
			data ["firstPoints"] = rabbitScore;
			data ["firstButtons"] = rabbitButtons;
		}
	
		data ["secondChar"] = secondPlace;
		if (secondPlace.Contains ("Fox")) {
			data ["secondPoints"] = foxScore;
			data ["secondButtons"] = foxButtons;
		}
		if(secondPlace.Contains ("Skunk")) {
			data ["secondPoints"] = skunkScore;
			data ["secondButtons"] = skunkButtons;
		}
		if(secondPlace.Contains ("Bear")) {
			data ["secondPoints"] = bearScore;
			data ["secondButtons"] = bearButtons;
		}
		if(secondPlace.Contains ("Rabbit")) {
			data ["secondPoints"] = rabbitScore;
			data ["secondButtons"] = rabbitButtons;
		}

		data ["thirdChar"] = thirdPlace;
		if (thirdPlace.Contains ("Fox")) {
			data ["thirdPoints"] = foxScore;
			data ["thirdButtons"] = foxButtons;
		}
		if(thirdPlace.Contains ("Skunk")) {
			data ["thirdPoints"] = skunkScore;
			data ["thirdButtons"] = skunkButtons;
		}
		if(thirdPlace.Contains ("Bear")) {
			data ["thirdPoints"] = bearScore;
			data ["thirdButtons"] = bearButtons;
		}
		if(thirdPlace.Contains ("Rabbit")) {
			data ["thirdPoints"] = rabbitScore;
			data ["thirdButtons"] = rabbitButtons;
		}

		data ["fourthChar"] = fourthPlace;
		if (fourthPlace.Contains ("Fox")) {
			data ["fourthPoints"] = foxScore;
			data ["fourthButtons"] = foxButtons;
		}
		if(fourthPlace.Contains ("Skunk")) {
			data ["fourthPoints"] = skunkScore;
			data ["fourthButtons"] = skunkButtons;
		}
		if(fourthPlace.Contains ("Bear")) {
			data ["fourthPoints"] = bearScore;
			data ["fourthButtons"] = bearButtons;
		}
		if(fourthPlace.Contains ("Rabbit")) {
			data ["fourthPoints"] = rabbitScore;
			data ["fourthButtons"] = rabbitButtons;
		}

		data["numItems"] = GM.itemsCollected.ToString ();
		data["numTraps"] = GM.trapsPlaced.ToString ();

		socket.Emit("dashboardPacket", new JSONObject(data));
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

	public void removeTrap(string ID){
		Dictionary<string, string> data = new Dictionary<string, string>();
		
		data["ID"] = ID;

		socket.Emit("deleteTrap", new JSONObject(data));
		Debug.Log ("trapsent id: " + ID);
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

	public void resetHouse() {
		socket.Emit("resetHouse");
	}
	
}
