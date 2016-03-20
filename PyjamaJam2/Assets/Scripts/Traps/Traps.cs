using UnityEngine;
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
			Debug.Log ("DELETE THIS TRAP" + lifeSpan);
            destroySelf();
        }
    }

    public void destroySelf()
    {
        if (gameObject != null) {
			Destroy (gameObject);
			Debug.Log ("TRAP DELETED");
		}
    }
}
