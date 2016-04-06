 using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public Items allItems;
    public GameMaster GM;
	public bool beenTriggered = false;
	public int difficulty;
	public Material diffuseMat;
	public Renderer rend;
	public GameObject itemIconPrefab;
	public GameObject itemIconRef;
	public bool isNightOnly;
	//public Character character;

    bool isDark = false;
	bool isHiding = false;
	bool isHidden = false;
	public int points = 10;
	float pickedUpTimer = 1f;

	// Use this for initialization
	protected virtual void Start () {
		GameObject goItems = GameObject.Find("GameMasterItems"); 
		allItems = goItems.GetComponent<Items> ();

        GameObject goMaster = GameObject.Find("GameMaster");
        GM = goMaster.GetComponent<GameMaster>();

		rend = GetComponent<Renderer>();
        if (isDark == true)
            this.gameObject.GetComponent<Animator>().SetBool("night", true);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Physics.IgnoreLayerCollision(9,11, true);

		if (isHiding == true && isHidden == false)
			Hide ();
	}

	public virtual void TriggerEffect(){
		if (itemIconRef) {
			Destroy (itemIconRef.gameObject);
		}
	}

	public void Hide(){
		if (isHiding == false) {
			Animator animator = this.gameObject.GetComponent<Animator> ();
			animator.SetBool ("isPickedUp", true);
			isHiding = true;
		}


		pickedUpTimer -= Time.deltaTime;

		if (pickedUpTimer < 0) {
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			isHidden = true;
		}

		if (itemIconPrefab & !itemIconRef) {
			itemIconRef = (GameObject)Instantiate (itemIconPrefab, new Vector3(0, -10, 0), Quaternion.identity);
		}

		//this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}

	public void inBindle(Vector3 charPos, Vector3 charRot){
		if (itemIconRef != null) {
			Vector3 iconPos = new Vector3 (charPos.x, charPos.y + 0.7f, charPos.z);
			itemIconRef.transform.position = iconPos;
			itemIconRef.transform.eulerAngles = charRot;
		}
	}


	public void doUpdate(){
		this.Update();
	}

	public void DestroySelf(){
		//allItems.removeItemFromArray (this.gameObject);
		Destroy(itemIconRef);
		allItems.removeItemFromArray(this.gameObject);
		Destroy(gameObject);
	}
    
	public void setDarkMode(){
        //this.gameObject.GetComponent<SpriteRenderer> ().material = diffuseMat;
        Debug.Log(this.gameObject);
        this.gameObject.GetComponent<Animator>().SetBool("night", true);
	}
    
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

	public void unusedItemCheck(){
		if (beenTriggered == false) {
			DestroySelf ();
		}
	}
}
