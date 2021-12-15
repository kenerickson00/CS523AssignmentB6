using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform agent;

    private Vector3 last;
    // Start is called before the first frame update
    void Start()
    {
        last = agent.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += agent.position - last;
        last = agent.position;
    }
}
