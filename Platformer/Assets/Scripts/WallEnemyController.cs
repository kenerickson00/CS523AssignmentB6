using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemyController : MonoBehaviour
{ //agent that travels around a wall, stopping to look when it reaches a corner waypoint
    public Transform waypoints;
    public float waitTime;
    public float switchTime;
    public float speed;
    public float raydist;
    public GameObject agent; //agent model

    UnityEngine.AI.NavMeshAgent nm;
    float tolerance = 0.1f;
    Animator animator;

    private int state; //0 wall following, 1 waiting, 2 detected the agent
    private int index;
    private float curTime;
    private Vector3 last;
    private List<Vector3> wpoints;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nm = GetComponent<UnityEngine.AI.NavMeshAgent>();

        index = 1; //already at index 0
        state = 0;
        curTime = 0.0f;
        if(raydist == 0)
            raydist = 4.0f;

        nm.speed = speed;
        nm.SetDestination(waypoints.GetChild(index).position);
        last = transform.position;

        wpoints = new List<Vector3>();
        for(int i=0;i<waypoints.childCount;i++)
        {
            wpoints.Add(waypoints.GetChild(i).position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 2)
        {
            nm.SetDestination(agent.transform.position);
            nm.isStopped = false;

            if (Vector3.Distance(last, transform.position) < tolerance)
            { //check if agent has escaped
                curTime += Time.deltaTime;
                if (curTime >= switchTime)
                {
                    state = 0;
                    nm.SetDestination(wpoints[index]);
                    nm.speed = speed;
                    curTime = 0.0f;
                    animator.SetFloat("speed", speed);
                }
            }
        }
        else
        {
            
            //raycast stuff
            bool rayhit = false;
            
            //RaycastHit hit;
           // float raydist = 4.0f; //arbitrary, may need to tweak this value
            Vector3 middle = transform.position + new Vector3(0, 0.5f, 0);
            Vector3 high = transform.position + new Vector3(0, 1.5f, 0);
            rayhit = CheckRaycast(middle);
            if(!rayhit)
                rayhit = CheckRaycast(high);
            // Debug.DrawRay(middle, transform.TransformDirection(Vector3.forward * raydist), Color.green);
            /**
            if (Physics.Raycast(middle, transform.TransformDirection(Vector3.forward), out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                {
                    rayhit = true;
                }
            }
            Vector3 or = Quaternion.Euler(0, 45, 0) * transform.TransformDirection(Vector3.forward);
            Vector3 ol = Quaternion.Euler(0, -22.5f, 0) * transform.TransformDirection(Vector3.forward);
            Vector3 ir = Quaternion.Euler(0, 22.5f, 0) * transform.TransformDirection(Vector3.forward);
            Vector3 il = Quaternion.Euler(0, -45, 0) * transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(middle, or, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                {
                    rayhit = true;
                }
            }
            if (Physics.Raycast(middle, ol, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                {
                    rayhit = true;
                }
            }
            if (Physics.Raycast(middle, ir, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                {
                    rayhit = true;
                }
            }
            if (Physics.Raycast(middle, il, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                {
                    rayhit = true;
                }
            }
            */

            if (rayhit)
            {
                agent.transform.parent.gameObject.GetComponent<AgentController>().setWarning();
                state = 2;
                curTime = 0.0f;
                nm.SetDestination(agent.transform.position);
                nm.speed = speed + 1.5f;
                nm.isStopped = false;
                animator.SetBool("Turn", false);
                animator.SetFloat("speed", 20.0f);
            }
            else
            {
                //other states
                if (state == 0)
                {
                    if(Vector3.Distance(transform.position, wpoints[index]) < tolerance)
                    {
                        nm.isStopped = true;
                        index += 1; //go to next location
                        if(index >= wpoints.Count) //we reached all points, loop around to start
                        {
                            index = 0;
                        }
                        state = 1;
                        curTime = 0.0f;
                        animator.SetFloat("speed", 0.0f);
                        animator.SetBool("Turn", true);
                    } else
                    {
                        animator.SetFloat("speed", speed);
                    }
                }
                else if (state == 1)
                {
                    curTime += Time.deltaTime;
                    if (curTime >= waitTime)
                    {
                        curTime = 0.0f;
                        state = 0;
                        nm.SetDestination(wpoints[index]);
                        nm.isStopped = false;
                        animator.SetBool("Turn", false);
                        animator.SetFloat("speed", speed);
                    }
                }
            }
        }
        /*float curspeed = (transform.position - last).magnitude / Time.deltaTime;
        animator.SetFloat("speed", curspeed);*/
        last = transform.position;
    }

    bool CheckRaycast(Vector3 rayToCast)
    {
            RaycastHit hit;
            //Debug.DrawRay(rayToCast, transform.TransformDirection(Vector3.forward * raydist), Color.green);
            if (Physics.Raycast(rayToCast, transform.TransformDirection(Vector3.forward), out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                    return true;
            }
            Vector3 or = Quaternion.Euler(0, 45, 0) * transform.TransformDirection(Vector3.forward);
            Vector3 ol = Quaternion.Euler(0, -22.5f, 0) * transform.TransformDirection(Vector3.forward);
            Vector3 ir = Quaternion.Euler(0, 22.5f, 0) * transform.TransformDirection(Vector3.forward);
            Vector3 il = Quaternion.Euler(0, -45, 0) * transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(rayToCast, or, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                    return true;
            }
            if (Physics.Raycast(rayToCast, ol, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                    return true;
            }
            if (Physics.Raycast(rayToCast, ir, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                    return true;
            }
            if (Physics.Raycast(rayToCast, il, out hit, raydist))
            {
                if (hit.collider.gameObject == agent)
                    return true;
            }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == agent) //check if agent has gotten too close
        {
            agent.transform.parent.gameObject.GetComponent<AgentController>().setWarning();
            state = 2;
            curTime = 0.0f;
            nm.SetDestination(agent.transform.position);
            nm.speed = speed + 1.5f;
            nm.isStopped = false;
            animator.SetBool("Turn", false);
            animator.SetFloat("speed", 20.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == agent) //game over, the agent has been caught
        {
            //Destroy(agent); //should replace this with some death animation or something
            agent.transform.parent.gameObject.GetComponent<AgentController>().getHit();
        }
        else if (collision.gameObject.tag == "Rocky") //enemy hazard
        {
            nm.speed = speed - 1.5f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Rocky") //enemy hazard
        {
            nm.speed = speed;
        }
    }
}
