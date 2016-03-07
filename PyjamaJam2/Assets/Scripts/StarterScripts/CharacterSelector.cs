using UnityEngine;using System.Collections;public class CharacterSelector : MonoBehaviour {    public StarterGM starterGM;    public int controllerNum;    int currentSelected = 0;    string playerControllerName;    string joystickX = "_JoystickX";    string buttonA = "_A";    bool joystickReset;    // Use this for initialization    void Start () {        playerControllerName = starterGM.playerControllerName;	}		// Update is called once per frame	void Update () {        if (starterGM.passedStartScreen == true)
        {
            //move character selector to right
            if (Input.GetAxis(playerControllerName + controllerNum + joystickX) > 0 && joystickReset == true)
            {
                if (currentSelected == starterGM.possibleCharacters.Count - 1)
                    currentSelected = 0;
                else
                    currentSelected++;
                transform.position = new Vector3(starterGM.possibleCharacters[currentSelected].transform.position.x, transform.position.y, 0f);
                joystickReset = false;
            }
            //move character selector to left
            else if (Input.GetAxis(playerControllerName + controllerNum + joystickX) < 0 && joystickReset == true)
            {
                if (currentSelected == 0)
                    currentSelected = 3;
                else
                    currentSelected--;
                transform.position = new Vector3(starterGM.possibleCharacters[currentSelected].transform.position.x, transform.position.y, 0f);
                joystickReset = false;
            }

            //reset the moving of selection
            if (Input.GetAxis(playerControllerName + controllerNum + joystickX) < 0.02 && Input.GetAxis(playerControllerName + controllerNum + joystickX) > -0.02)
            {
                joystickReset = true;
            }

            //select character
            if (Input.GetButtonDown(playerControllerName + controllerNum + buttonA))
            {
                starterGM.setCharToController(currentSelected, controllerNum);
            }        }    }}