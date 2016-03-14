using UnityEngine;
using System.Collections;

public class Latern : MonoBehaviour {
	public bool isActivated = false;
	Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void activateLatern(){
		//start latern
		light.range = 40;
		isActivated = true;
	}

	public void updateWithCharData(float xPos, float yPos){
		Vector3 newPos = new Vector3(xPos, yPos, gameObject.transform.position.z + 0.003f);
		gameObject.transform.position = newPos;

        light.range -= 0.0007f;
        light.intensity -= 0.0007f;
    }

	public void lightBoost(){
		light.range += 10f;
		light.intensity += 0.30f;
	}


}
