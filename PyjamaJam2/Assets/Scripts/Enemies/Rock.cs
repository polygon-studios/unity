using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	bool thrown = false;
	public Rigidbody2D rb;

	float timer = 10; //in seconds

	Vector2 force = new Vector2 (3, 1);
	float thrust = 1.0f;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(12,13, true);
		Physics2D.IgnoreLayerCollision(12,12, true);
		Physics2D.IgnoreLayerCollision(12,11, true);
	}

	void Awake() {

	}
	
	// Update is called once per frame
	void Update () {

		if (!thrown) {
			Rigidbody2D rb = GetComponent<Rigidbody2D>();
			rb.AddForce(force, ForceMode2D.Impulse);
			thrown = true;
		}
		timer -= Time.deltaTime;
		if (timer < 0) {
			if(gameObject != null)
				Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D objectHit){
		if (objectHit.gameObject.tag == "ground") {
			destroySelf();
		}
	}

	public void destroySelf()
	{
		if (gameObject != null) 
			Destroy(gameObject);
	}
}
