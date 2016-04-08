using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Traps : MonoBehaviour {

	public TrapMaster allTraps;
	public SocketIOLogic socketIO;
	public float lifeSpan = 40f; //in seconds

	public string trapID;
	
	void Start(){
		GameObject go = GameObject.FindWithTag("SocketIOLogic");
		if (go) {
			socketIO = go.GetComponent<SocketIOLogic> ();
		}

		GameObject trapMaster  = GameObject.FindWithTag("GameMasterTraps");
		if (go) {
			allTraps = trapMaster.GetComponent<TrapMaster> ();
		}


	}
	
	// Update is called once per frame
	void Update()
	{
		lifeSpan -= Time.deltaTime;
		
		if (lifeSpan < 0)
		{
			destroySelf();
		}
	}
	
	public void destroySelf()
	{
		if (gameObject != null) {
			socketIO.removeTrap(trapID);
			allTraps.removeItemFromArray(this.gameObject);
			Destroy (gameObject);
		}
	}
}
