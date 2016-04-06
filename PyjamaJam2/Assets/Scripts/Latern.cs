using UnityEngine;
using System.Collections;

public class Latern : MonoBehaviour {
	public bool isActivated = false;
	Light light1;

	// Use this for initialization
	void Start () {
		light1 = GetComponent<Light> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void activateLatern(){
		//start latern
		light1.range = 40;
		isActivated = true;

	}

	public void updateWithCharData(float xPos, float yPos){
		Vector3 newPos = new Vector3(xPos, yPos, gameObject.transform.position.z);

		//Debug.Log (light1.intensity + "  " + gameObject.transform.position.z + "     " + light1.range);
		if ((light1.intensity > 0.7 && gameObject.transform.position.z < -1.2f && light1.range > 37)) {

			light1.range -=  0.0005f; //  0.0005f
			light1.intensity -= 0.0004f; //0.0004f
			newPos = new Vector3(xPos, yPos, gameObject.transform.position.z + 0.003f); //0.003f


		}

		gameObject.transform.position = newPos;
    }

   /* public bool checkLightBrightness()
    {
		if(light1.range < 7 || light1.intensity < 0.01 || gameObject.transform.position.z > -0.5f)
        {
            Debug.Log("LOW");
            return false;
        }
        return true;
    }*/

    public void resetLight(float xPos, float yPos)
    {
		light1.range = 30;
		light1.intensity = 1.75f;
        Vector3 newPos = new Vector3(xPos, yPos, -6f);
        gameObject.transform.position = newPos;
    }

	public void lightBoost(){
		light1.range += 16f;
		if(light1.intensity < 2f)
			light1.intensity += 0.25f;

        Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 2.5f);
        gameObject.transform.position = newPos;
    }


}
