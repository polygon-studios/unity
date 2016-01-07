using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
    public Character foxChar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D objectHit){
        if(objectHit.gameObject.tag == "characterFOX"){
            foxChar.isOnGround(true);
        }
    }
}
