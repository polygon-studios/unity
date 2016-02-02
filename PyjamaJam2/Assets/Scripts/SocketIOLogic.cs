﻿using UnityEngine;
using SocketIO;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class SocketIOLogic : MonoBehaviour
{
	private SocketIOComponent socket;
	public GameObject trapPrefab;
	
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
		
		StartCoroutine(BeepBoop(1.0f));
	}

	public void Update()
	{
		//socket.On("open", TestOpen);
		//socket.On("news", TestBoop);
		//socket.On("success", TestBoop);
		//socket.On("error", TestError);
		//socket.On("close", TestClose);
		//socket.Emit("beep");
		//socket.Emit("open");
		//socket.On("Position", TestPosition);
		//socket.On("boop", TestBoop);
		getFoxPosition ();
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
		Debug.Log("[SocketIO] Trap place received: " + e.name + " " + e.data);
		Debug.Log("Trap x pos:" + e.data["pos-x"] + " trap y pos: " + e.data["pos-y"]);


		string tempx = string.Format ("{0}", e.data ["pos-x"]);
		float xPos = (float.Parse (tempx)) * 0.733f;
		string tempy = string.Format ("{0}", e.data ["pos-y"]);
		float yPos = (float.Parse (tempy)) * 0.777f;


		GameObject trap = (GameObject)Instantiate (trapPrefab, new Vector3 (xPos, yPos, 0), Quaternion.identity);

		if (e.data == null) { return; }
		
		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
			);
	}


	/*
	 * 
	 * Non- socket.io functions
	 * 
	 */
	public void getFoxPosition() {
		GameObject[] Players = GameObject.FindGameObjectsWithTag("character");
		
		foreach (GameObject character in Players) {
			float positionX = character.transform.position.x;
			float positionY = character.transform.position.y;
			Dictionary<string, string> data = new Dictionary<string, string>();
			if(character.name == "character_Fox"){
				data["fox-X"] = positionX.ToString();
				data["fox-Y"] = positionY.ToString();
			}

			Debug.Log (data);
		}


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

}
