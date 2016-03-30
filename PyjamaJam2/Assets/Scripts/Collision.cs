using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {

    public AudioClip audioEffectCoinHit; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D objectHit)
    {
        if (objectHit.gameObject.tag == "character")
        {
            Character character = objectHit.gameObject.GetComponent<Character>();
            if (character.isFalling() == true){               
                character.isOnGround(true);
				character.justLanded ();
            }
        }
        if (objectHit.gameObject.tag == "coin")
        {
           GameObject coin = objectHit.gameObject;
           Vector2 coinPos = new Vector2(coin.transform.position.x, coin.transform.position.y);
           AudioSource audioSource = gameObject.AddComponent<AudioSource>();
           AudioClip audioEffectCoinHit = (AudioClip)Resources.Load("Coin Drop") as AudioClip;
           audioSource.PlayOneShot(audioEffectCoinHit, 0.1f);
       }
    }
}
