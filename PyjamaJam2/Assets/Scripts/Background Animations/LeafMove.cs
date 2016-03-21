using UnityEngine;
using System.Collections;

public class LeafMove : MonoBehaviour
{
    Vector3 target;
    Vector3 initPos;
    float speed;
    //float speed = 0.5f;

    //GameObject[] targetsList;

    float timeLeft;

    // Use this for initialization
    void Start()
    {
        //targetsList = GameObject.FindGameObjectsWithTag("CloudTarget");
        initPos = this.transform.position;
        target = new Vector3(initPos.x, 0.25f, initPos.z);
        speed = Random.Range(0.1f, 0.5f);
        timeLeft = Random.Range(0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(transform.position, target, step);

        Vector3 pos = this.transform.position;

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                resetLeaf();
                timeLeft = Random.Range(0, 30);
            }
        }
    }

    void resetLeaf()
    {
        //Vector3 newPos = new Vector3(initPos.x, initPos.position.y, initPos.position.z);
        transform.position = initPos;
        speed = Random.Range(0.1f, 0.5f);
        print("reset leaf");
    }
}


