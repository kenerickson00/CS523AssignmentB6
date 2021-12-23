using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableFloorController : MonoBehaviour
{
    public float maxTime;

    Rigidbody rb;

    private float timer;
    private bool start;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        timer = 0.0f;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            timer += Time.deltaTime;
            if(timer >= maxTime)
            {
                rb.isKinematic = false;
                start = false;
                timer = 0.0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            start = true;
        }
    }
}
