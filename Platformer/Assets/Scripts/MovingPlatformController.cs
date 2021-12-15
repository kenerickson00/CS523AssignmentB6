using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public int axis; //0 x, 1 y, 2 z
    public bool flip; //we normally go up/foward first, if false we go down/back first instead (if flip is true)
    public float dist; //distance we travel
    public float timer; //time it takes to travel

    private float curTime;
    private int dir;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0.0f;
        if(flip)
        {
            dir = -1;
        } else
        {
            dir = 1;
        }
        speed = dist / timer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 temp = new Vector3(0, 0, 0);
        temp[axis] += dir * speed * Time.deltaTime;
        transform.position += temp;
        curTime += Time.deltaTime;
        if(curTime >= timer)
        {
            curTime = 0.0f;
            dir = dir * -1;
        }
    }
}
