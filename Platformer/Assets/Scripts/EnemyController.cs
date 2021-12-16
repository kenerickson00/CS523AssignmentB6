using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ //basic agent that goes back and forth between two points
    public Transform dest;
    public float waitTime;
    public bool flip; //go back instead of forward first
    public float speed;

    UnityEngine.AI.NavMeshAgent nm;
    float tolerance = 0.1f;

    private int state; //0 walking, 1 waiting, 2 detected the agent
    private Vector3 origin;
    private Vector3 destination;
    private bool dir;
    private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<UnityEngine.AI.NavMeshAgent>();

        dir = true; //foward
        origin = transform.position;
        destination = dest.position;
        state = 0;
        curTime = 0.0f;

        nm.speed = speed;
        nm.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            if (dir)
            {
                if (Vector3.Distance(transform.position, destination) < tolerance)
                {
                    nm.isStopped = true;
                    dir = false;
                    state = 1;
                    curTime = 0.0f;
                    //play waiting/turning animation
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, origin) < tolerance)
                {
                    nm.isStopped = true;
                    dir = true;
                    state = 1;
                    curTime = 0.0f;
                    //play waiting/turning animation
                }
            }
        } else if(state == 1)
        {
            curTime += Time.deltaTime;
            if(curTime >= waitTime)
            {
                curTime = 0.0f;
                state = 0;
                if(dir)
                {
                    nm.SetDestination(destination);
                } else
                {
                    nm.SetDestination(origin);
                }
                nm.isStopped = false;
            }
        }
    }
}
