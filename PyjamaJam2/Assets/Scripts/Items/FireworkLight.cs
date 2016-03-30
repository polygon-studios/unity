using UnityEngine;
using System.Collections;

public class FireworkLight : MonoBehaviour {

    public GameObject firework;
    Light light;
    public float totalTimer;
	public float explosionTimer;
    bool fireworkTriggered;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.range = 0;
		fireworkTriggered = true;
		firework = (GameObject)Instantiate (firework, new Vector3 (transform.position.x, transform.position.y, firework.gameObject.transform.position.z), Quaternion.identity);
        //firework.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, firework.gameObject.transform.position.z);
		light.range = 15f;
	}
	
	// Update is called once per frame
	void Update () {
		explosionTimer -= Time.deltaTime;
		totalTimer -= Time.deltaTime;
		if (explosionTimer > 0)
        {
			light.range += 1f;
        }
		if (totalTimer < 0) {
			destroySelf ();
		}
	}

    public void resetFirework()
    {
        fireworkTriggered = false;
        light.range = 0;
    }

	void destroySelf(){
		Destroy (firework);
		Destroy (this.gameObject);
	}
}
