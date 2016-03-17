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
            if (character.isFalling() == true)
            {
                character.isOnGround(true);
            }
            if (objectHit.gameObject.tag == "coin")
            {
                GameObject coin = objectHit.gameObject;
                Vector2 coinPos = new Vector2(coin.transform.position.x, coin.transform.position.y);
                //x value: coinPos.x;
                //y value: coinPos.y;
                //play coin audio
            }
        }
    }
}
