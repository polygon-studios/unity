using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

	public List<Vector2> controllerToCharacter = new List<Vector2> (); //x is controller #, y is character # (0 is fox, 1 is skunk, 2, is rabbit, 3 is bear);

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		
			DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
