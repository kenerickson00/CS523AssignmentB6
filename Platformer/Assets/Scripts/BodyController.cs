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
        if (collision.gameObject.tag == "Platform")
        {
            if (par.checkJumping())
            {
                par.setJumpUp(false);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (!par.checkJumping()) //if we fell off a platform but didn't jump, make sure we can't jump midair
            {
                par.setJumpUp(true);
            }
        }
    }
}
