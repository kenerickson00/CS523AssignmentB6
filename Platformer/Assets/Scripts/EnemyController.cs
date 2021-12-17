using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ //basic agent that goes back and forth between two points
    public Transform dest;
    public float waitTime;
    public float switchTime;
    public float speed;
    public GameObject agent; //agent model

    UnityEngine.AI.NavMeshAgent nm;
    float tolerance = 0.1f;
    Animator animator;

    private int state; //0 walking, 1 waiting, 2 detected the agent
    private Vector3 origin;
    private Vector3 destination;
    private bool dir;
    private float curTime;
    private Vector3 last;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nm = GetComponent<UnityEngine.AI.NavMeshAgent>();

        dir = true; //foward
        origin = transform.position;
        destination = dest.position;
        state = 0;
        curTime = 0.0f;

        nm.speed = speed;
        nm.SetDestination(destination);
        last = transform.position;
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
                    nm.SetDestination(destination);
                    animator.SetFloat("speed", speed);
                    nm.speed = speed;
                    dir = true;
                    curTime = 0.0f;
                }
            } /*else
            {
                float curspeed = (transform.position - last).magnitude / Time.deltaTime;
                animator.SetFloat("speed", curspeed);
            }*/
            last = transform.position;
        }
        else
        {
            //raycast stuff
            bool rayhit = false;
            RaycastHit hit;
            float raydist = 6.0f; //arbitrary, may need to tweak this value
            Vector3 middle = transform.position + new Vector3(0, 0.5f, 0);
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

            if (rayhit)
            {
                state = 2;
                curTime = 0.0f;
                nm.speed = speed + 1.5f;
                nm.SetDestination(agent.transform.position);
                animator.SetFloat("speed", 20.0f);
                nm.isStopped = false;
            }
            else
            {
                //other states
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
                            animator.SetFloat("speed", 0.0f);
                        } else
                        {
                            animator.SetFloat("speed", speed);
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
                            animator.SetFloat("speed", 0.0f);
                        } else
                        {
                            animator.SetFloat("speed", speed);
                        }
                    }
                }
                else if (state == 1)
                {
                    curTime += Time.deltaTime;
                    if (curTime >= waitTime)
                    {
                        curTime = 0.0f;
                        state = 0;
                        if (dir)
                        {
                            nm.SetDestination(destination);
                        }
                        else
                        {
                            nm.SetDestination(origin);
                        }
                        animator.SetFloat("speed", speed);
                        nm.isStopped = false;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == agent) //check if agent has gotten too close
        {
            state = 2;
            curTime = 0.0f;
            nm.speed = speed + 1.5f;
            nm.SetDestination(agent.transform.position);
            animator.SetFloat("speed", 20.0f);
            nm.isStopped = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == agent) //game over, the agent has been caught
        {
            Destroy(agent); //should replace this with some death animation or something
        } else if(collision.gameObject.tag == "Rocky") //enemy hazard
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
