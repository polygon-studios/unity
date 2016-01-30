using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D objectHit){
        if(objectHit.gameObject.tag == "character"){
			Character character = objectHit.gameObject.GetComponent<Character>();
			character.isOnGround(true);
        }
    }
}
