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

        light.range -= 0.0005f;
        light.intensity -= 0.0004f;
    }

    public bool checkLightBrightness()
    {
        if(light.range < 7 || light.intensity < 0.01 || gameObject.transform.position.z > -0.5f)
        {
            Debug.Log("LOW");
            return false;
        }
        return true;
    }

    public void resetLight(float xPos, float yPos)
    {
        light.range = 30;
        light.intensity = 2.5f;
        Vector3 newPos = new Vector3(xPos, yPos, -6f);
        gameObject.transform.position = newPos;
    }

	public void lightBoost(){
		light.range += 16f;
		light.intensity += 0.25f;

        Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 2.5f);
        gameObject.transform.position = newPos;
    }


}
