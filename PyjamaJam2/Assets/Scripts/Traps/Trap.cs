using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	public Traps allItems;
	int points = 10;
	public bool beenTriggered = false;
	
	// Use this for initialization
	protected virtual void Start () {
		//GameObject goItems = GameObject.Find("GameMasterItems");    
		//allItems = goItems.GetComponent<Traps> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
	
	public virtual void TriggerEffect(){
		
	}
	
	public void Hide(){
		this.gameObject.GetComponent<Renderer>().enabled = false;
		this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
	
	
	public void doUpdate(){
		this.Update();
	}
	
	public void DestroySelf(){
		Destroy(gameObject);
	}
}
