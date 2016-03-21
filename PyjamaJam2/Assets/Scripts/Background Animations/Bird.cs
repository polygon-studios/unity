using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
    Transform target;
    public float speed;

    GameObject[] targetsList;

    float timeLeft = Random.Range(0, 30);

    // Use this for initialization
    void Start () {
        targetsList = GameObject.FindGameObjectsWithTag("BirdTarget");
        generateTarget();
    }

    // Update is called once per frame
    void Update () {
        float step = speed * Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        Vector3 pos = this.transform.position;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                resetBird();
                timeLeft = Random.Range(0, 30);
            }
        }
    }

    void generateTarget()
    {
        target = targetsList[Random.Range(0, targetsList.Length)].transform;
    }

    void resetBird()
    {
        Vector3 newPos = new Vector3(30, Random.Range(3, 7));
        transform.position = newPos;
        generateTarget();
    }
}


