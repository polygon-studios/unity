using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Traps : MonoBehaviour {
	
	public static Traps TRAPS;
    public List<GameObject> TRAPARRAY = new List<GameObject>();
    public float lifeSpan = 40f; //in seconds
	
	void Awake(){
		if (TRAPS != null)
			GameObject.Destroy (TRAPS);
		else
			TRAPS = this;
		DontDestroyOnLoad (this);
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


    public void removeItemFromArray(GameObject itemObj){
		TRAPARRAY.Remove (itemObj);
	}
}
