using System.Collections;
using UnityEngine;
using SocketIO;

public class SocketIOLogic : MonoBehaviour
{
	private SocketIOComponent socket;
	
	public void Start() 
	{
		GameObject go = GameObject.FindWithTag("SocketIO");
		if (go) {
			socket = go.GetComponent<SocketIOComponent> ();
		}
		socket.On("open", TestOpen);
		socket.On("news", TestBoop);
		socket.On("success", TestBoop);
		socket.On("Position", TestPosition);
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
	}
	
	IEnumerator BeepBoop(float disValue)
	{
		Debug.Log ("Testing beepboop");
		// wait 1 seconds and continue
		yield return new WaitForSeconds(1);
		
		socket.Emit("open");
		Debug.Log ("Opendone");
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
		Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);
		
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
