using UnityEngine;
using System.Collections;

public class IconBackground : MonoBehaviour {

	public Sprite defaultIcon;
	public Sprite player1Icon;
	public Sprite player2Icon;
	public Sprite player3Icon;
	public Sprite player4Icon;

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void isSelected(int controllerNumber){
		switch (controllerNumber) {
		case 1:
			spriteRenderer.sprite = player1Icon;
			break;
		case 2:
			spriteRenderer.sprite = player2Icon;
			break;
		case 3:
			spriteRenderer.sprite = player3Icon;
			break;
		case 4:
			spriteRenderer.sprite = player4Icon;
			break;
		}
	}

	public void deSelected(){
		spriteRenderer.sprite = defaultIcon;
	}
}
