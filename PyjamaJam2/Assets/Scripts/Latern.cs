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
		light.range -= 0.005f;
		light.intensity -= 0.0002f;
	}

	public void activateLatern(){
		//start latern
		light.range = 10;
		isActivated = true;
	}

	public void updateWithCharData(float xPos, float yPos){
		Vector3 newPos = new Vector3(xPos, yPos, gameObject.transform.position.z);
		gameObject.transform.position = newPos;
	}

	public void lightBoost(){
		light.range += 10f;
		light.intensity += 0.30f;
	}


}
