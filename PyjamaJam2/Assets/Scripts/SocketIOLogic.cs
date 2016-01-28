using UnityEngine;
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
		socket.On("trapPlace", TestPosition);
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
	}
	
	IEnumerator BeepBoop(float disValue)
	{
		Debug.Log ("Testing beepboop");

		// wait 3 seconds and continue
		yield return new WaitForSeconds(3);
		
		socket.Emit("beep");
		Debug.Log ("Beepdone");
		// wait 2 seconds and continue
		yield return new WaitForSeconds(2);
		
		socket.Emit("getPositions");
		Debug.Log ("getPositions done");
		// wait ONE FRAME and continue
		yield return null;
		
		socket.Emit("getPositions");
		socket.Emit("getPositions");
	}
	
	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
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

	public void TestPosition(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Trap place received: " + e.name + " " + e.data);
		Debug.Log("Trap x pos:" + e.data["pos-x"] + " trap y pos: " + e.data["pos-y"]);


		Debug.Log (e.data ["pos-x"]);
		GameObject trap = (GameObject)Instantiate (trapPrefab, new Vector3 (14.0f, 4.0f, 0), Quaternion.identity);

		if (e.data == null) { return; }
		
		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
			);
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
