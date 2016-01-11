 using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	int points = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hide(){
  		this.gameObject.GetComponent<Renderer>().enabled = false;
		this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}

	public void doUpdate(){
		this.Update();
	}
}
