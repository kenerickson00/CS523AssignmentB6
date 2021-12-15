using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public AgentController par;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Rocky")
        {
            if (par.checkJumping())
            {
                par.setJumpUp(false);
            }
            par.setMud(false);
        } else if (collision.gameObject.tag == "Mud")
        {
            if (par.checkJumping())
            {
                par.setJumpUp(false);
            }
            par.setMud(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Mud" || collision.gameObject.tag == "Rocky")
        {
            if (!par.checkJumping()) //if we fell off a platform but didn't jump, make sure we can't jump midair
            {
                par.setFalling();
            }
        }
        par.setMud(false);
    }
}
