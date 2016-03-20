﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Traps : MonoBehaviour {

    public float lifeSpan = 40f; //in seconds
	
	void Start(){
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
        if (gameObject != null) 
            Destroy(gameObject);
    }
}
