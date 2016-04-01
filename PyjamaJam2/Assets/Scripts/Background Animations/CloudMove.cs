using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour
{
    Transform target;
    float speed;
    //float speed = 0.5f;

    GameObject[] targetsList;

    float timeLeft = Random.Range(0, 30);

    // Use this for initialization
    void Start()
    {
        targetsList = GameObject.FindGameObjectsWithTag("CloudTarget");
        generateTarget();
        speed = Random.Range(0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        Vector3 pos = this.transform.position;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                resetCloud();
                timeLeft = Random.Range(0, 30);
            }
        }
    }

    void generateTarget()
    {
        target = targetsList[Random.Range(0, targetsList.Length)].transform;
    }

    void resetCloud()
    {
        Vector3 newPos = new Vector3(32, Random.Range(4, 6), 0.1f);
        transform.position = newPos;
        generateTarget();
        speed = Random.Range(0.1f, 0.5f);
    }
}


