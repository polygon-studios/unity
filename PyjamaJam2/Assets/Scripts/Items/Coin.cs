using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public float lifeSpan = 20f; //in seconds
	public int mass;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifeSpan -= Time.deltaTime;
		
		if (lifeSpan < 0) {
			destroySelf();
		}
	}

	public void destroySelf(){
        if(gameObject != null)
		    Destroy(gameObject);
	}
}
