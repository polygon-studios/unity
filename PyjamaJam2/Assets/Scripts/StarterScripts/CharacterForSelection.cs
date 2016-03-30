using UnityEngine;
using System.Collections;

public class CharacterForSelection : MonoBehaviour {

	public IconBackground iconBG;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void isSelected(int controllerNum){
		iconBG.isSelected (controllerNum);
	}

	public void deSelected(){
		iconBG.deSelected ();
	}
}
