using UnityEngine;
using System.Collections;

public class FireworkLight : MonoBehaviour {

    public GameObject firework;
    Light light;
    public float timer;
    bool fireworkTriggered;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.range = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (fireworkTriggered == true)
        {
            light.range = 32;
        }
	}

    public void resetFirework()
    {
        fireworkTriggered = false;
        light.range = 0;
    }
}
