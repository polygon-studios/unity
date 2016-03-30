using UnityEngine;
using System.Collections;

public class CharacterSelector : MonoBehaviour {
    //character selection
	public StarterGM starterGM;
	bool characterSelected = false;
    int currentSelected = 0;

	public float offsetX;

	//controls
	public int controllerNum;
    string playerControllerName;
    string joystickX = "_JoystickX";
    string buttonA = "_A";
	string buttonB = "_B";

    bool joystickReset;
    

    // Use this for initialization
    void Start () {
        playerControllerName = starterGM.playerControllerName;
	}
	
	// Update is called once per frame
	void Update () {
        if (starterGM.passedStartScreen == true)
        {
			if (characterSelected == false) {
				//move character selector to right
				if (Input.GetAxis (playerControllerName + controllerNum + joystickX) > 0 && joystickReset == true) {
					bool onUnselectedChar = false;
					while (onUnselectedChar == false) {
						if (currentSelected == starterGM.possibleCharacters.Count - 1)
							currentSelected = 0;
						else
							currentSelected++;
						if (starterGM.isSelectedCharacter [currentSelected] == false)
							onUnselectedChar = true;
					}
					transform.position = new Vector3 (starterGM.possibleCharacters [currentSelected].transform.position.x + offsetX, transform.position.y, 0f);
					joystickReset = false;
				}
            	//move character selector to left
            	else if (Input.GetAxis (playerControllerName + controllerNum + joystickX) < 0 && joystickReset == true) {
					bool onUnselectedChar = false;
					while (onUnselectedChar == false) {
						if (currentSelected == 0)
							currentSelected = 3;
						else
							currentSelected--;
						if (starterGM.isSelectedCharacter [currentSelected] == false)
							onUnselectedChar = true;
					}
					transform.position = new Vector3 (starterGM.possibleCharacters [currentSelected].transform.position.x + offsetX, transform.position.y, 0f);
					joystickReset = false;
				}

				//reset the moving of selection
				if (Input.GetAxis (playerControllerName + controllerNum + joystickX) < 0.02 && Input.GetAxis (playerControllerName + controllerNum + joystickX) > -0.02) {
					joystickReset = true;
				}
			
	            //select character
				if (Input.GetButtonDown(playerControllerName + controllerNum + buttonA))
	            {
					if (starterGM.isSelectedCharacter [currentSelected] == false) {
						starterGM.setCharToController (currentSelected, controllerNum);
						//starterGM.updateScreen (currentSelected);
						characterSelected = true;
					}
	            }
			}

			//re-select new character
			if (Input.GetButtonDown (playerControllerName + controllerNum + buttonB) && characterSelected == true) {
				starterGM.removeSelection (currentSelected);
				characterSelected = false;
			}
        }



    }
}
