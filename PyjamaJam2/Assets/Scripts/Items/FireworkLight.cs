﻿using UnityEngine;
using System.Collections;

public class FireworkLight : MonoBehaviour {
    public GameObject firework;
    Light light1;
    public float totalTimer;
	public float explosionTimer;
    bool fireworkTriggered;

	// Use this for initialization
	void Start () {
        light1 = GetComponent<Light>();
		light1.range = 0;
		fireworkTriggered = true;
		firework = (GameObject)Instantiate (firework, new Vector3 (transform.position.x, transform.position.y, firework.gameObject.transform.position.z), Quaternion.identity);


        //firework.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, firework.gameObject.transform.position.z);
		light1.range = 15f;
	}
	
	// Update is called once per frame
	void Update () {
		explosionTimer -= Time.deltaTime;
		totalTimer -= Time.deltaTime;
		if (explosionTimer > 0)
        {
			light1.range += 1f;
        }
		if (totalTimer < 0) {
			destroySelf ();
		}
	}

    public void resetFirework()
    {
        fireworkTriggered = false;
		light1.range = 0;
    }

	void destroySelf(){
		Destroy (firework);
		Destroy (this.gameObject);
	}
}
