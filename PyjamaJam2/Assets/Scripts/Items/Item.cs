﻿ using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public Items allItems;
	public bool beenTriggered = false;
	public int difficulty;
	public Material diffuseMat;

	int points = 10;

	// Use this for initialization
	protected virtual void Start () {
		GameObject goItems = GameObject.Find("GameMasterItems");    
		allItems = goItems.GetComponent<Items> ();
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
		allItems.removeItemFromArray (this.gameObject);
		Destroy(gameObject);
	}

	public void setDarkMode(){
		this.gameObject.GetComponent<SpriteRenderer> ().material = diffuseMat;
	}
}
