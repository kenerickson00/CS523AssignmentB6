using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    Rigidbody rb;
    Transform agentBody; //visual model of agent

    private bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agentBody = transform.GetChild(0);

        jumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hdir = Input.GetAxis("Horizontal");
        float vdir = Input.GetAxis("Vertical");

        transform.position += speed * Time.deltaTime * new Vector3(hdir, 0, vdir);

        //handle rotation. Should probably do these turns with animations and not just abrupt turns, but this is more or less how we want it to work
        if(vdir > 0) //forwards
        {
            if(hdir > 0) //NE
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 45, agentBody.eulerAngles.z);
            } else if(hdir < 0) //NW
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, -45, agentBody.eulerAngles.z);
            } else //hdir == 0 N
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 0, agentBody.eulerAngles.z);
            }
        } else if(vdir < 0) //backwards
        {
            if (hdir > 0) //SE
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 135, agentBody.eulerAngles.z);
            }
            else if (hdir < 0) //SW
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, -135, agentBody.eulerAngles.z);
            }
            else //hdir == 0 S
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 180, agentBody.eulerAngles.z);
            }
        } else // vdir == 0
        {
            if (hdir > 0) //E
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 90, agentBody.eulerAngles.z);
            }
            else if (hdir < 0) //W
            {
                agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, -90, agentBody.eulerAngles.z);
            } //else not moving, dont need to turn
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            jumping = true;
        }
        /* Some brief thoughts about how movement should work
         * For non jumping movement, ie moving forward, backwards, and sideways, we should do it using transforms
         *  because this will keep movement quick and responsive, we don't want to be waiting to accelerate for a long 
         *  time when we're trying to run away. We could do a little acceleration, but we want to start at near max
         *  speed
         * To account for turning, we should turn the agent model (agentBody variable) but not the transform of the main
         *  agent itself. In the last animation assignment the movement was pretty unsatisfying because we couldn't just
         *  move diagonally normally, we had to turn to do that, but in normal 3rd person games you can just move 
         *  diagonally without any trouble. To do this we can just have normal movement and turn the model to face the
         *  direction of our movement.
         * For vertical/jumping movement we should use rigidbody forces as this will do all of the gravity work for us
         *  which should make things a lot easier. We just need to have a state system to make sure that you can't 
         *  jump too many times in a row
         */
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            if(jumping)
            {
                jumping = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (!jumping)
            {
                jumping = true;
            }
        }
    }
}
