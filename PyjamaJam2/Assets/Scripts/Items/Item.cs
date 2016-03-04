 using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public Items allItems;
    public GameMaster GM;
	public bool beenTriggered = false;
	public int difficulty;
	public Material diffuseMat;
	public Renderer rend;

	int points = 10;

	// Use this for initialization
	protected virtual void Start () {
		GameObject goItems = GameObject.Find("GameMasterItems"); 
		allItems = goItems.GetComponent<Items> ();

        GameObject goMaster = GameObject.Find("GameMaster");
        GM = goMaster.GetComponent<GameMaster>();

		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Physics.IgnoreLayerCollision(9,11, true);
	}

	public virtual void TriggerEffect(){

	}

	public void Hide(){
  		this.gameObject.GetComponent<Renderer>().enabled = false;
		this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}


	public void doUpdate(){
		this.Update();
	}

	public void DestroySelf(){
		allItems.removeItemFromArray (this.gameObject);
		Destroy(gameObject);
	}

	public void setDarkMode(){
		this.gameObject.GetComponent<SpriteRenderer> ().material = diffuseMat;
	}

	// Subtracts health from character when hit
	public Vector2 getItemPosition()
	{
		Vector2 pos = transform.position;
		return pos;
	}
	
	public float getItemRadius()
	{
		float radius = rend.bounds.extents.magnitude;
		return radius;
	}
}
