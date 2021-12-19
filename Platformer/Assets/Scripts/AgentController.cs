using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float mudPenalty;
    public float warnTimer;
    public Image health;
    public Image defeated;
    public Sprite healthMissing;

    Rigidbody rb;
    Transform agentBody; //visual model of agent
    Animator animator;

    private bool jumping;
    private bool onMud;
    private bool gameover;
    private bool victory;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        agentBody = transform.GetChild(0);
        rb = agentBody.gameObject.GetComponent<Rigidbody>();
        animator = agentBody.gameObject.GetComponent<Animator>();
        jumping = false;
        onMud = false;
        gameover = false;
        victory = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false); //close warning
            }
        }

        if (!gameover && !victory)
        {
            float hdir = Input.GetAxis("Horizontal");
            float vdir = Input.GetAxis("Vertical");

            if (onMud)
            {
                transform.position += mudPenalty * speed * Time.deltaTime * new Vector3(hdir, 0, vdir);
            }
            else
            {
                transform.position += speed * Time.deltaTime * new Vector3(hdir, 0, vdir);
            }

            //handle rotation. Should probably do these turns with animations and not just abrupt turns, but this is more or less how we want it to work
            if (vdir > 0) //forwards
            {
                if (hdir > 0) //NE
                {
                    agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 45, agentBody.eulerAngles.z);
                }
                else if (hdir < 0) //NW
                {
                    agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, -45, agentBody.eulerAngles.z);
                }
                else //hdir == 0 N
                {
                    agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 0, agentBody.eulerAngles.z);
                }
                animator.SetBool("moving", true);
            }
            else if (vdir < 0) //backwards
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
                animator.SetBool("moving", true);
            }
            else // vdir == 0
            {
                if (hdir > 0) //E
                {
                    agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, 90, agentBody.eulerAngles.z);
                    animator.SetBool("moving", true);
                }
                else if (hdir < 0) //W
                {
                    agentBody.eulerAngles = new Vector3(agentBody.eulerAngles.x, -90, agentBody.eulerAngles.z);
                    animator.SetBool("moving", true);
                }
                else
                { //else not moving, dont need to turn
                    animator.SetBool("moving", false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0));
                jumping = true;
                animator.SetBool("jumpUp", true);
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
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.tag == "Platform")
        {
            if(jumping)
            {
                jumping = false;
                //animator.SetBool("jumpUp", false);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (!jumping) //if we fell off a platform but didn't jump, make sure we can't jump midair
            {
                jumping = true;
            }
        }
    }*/

    public void setJumpUp(bool b)
    {
        if (b)
        {
            jumping = true;
            animator.SetBool("jumpUp", true);
        }
        else
        {
            jumping = false;
            animator.SetBool("jumpUp", false);
        }
    }

    public void setFalling()
    {
        jumping = true;
    }

    public bool checkJumping()
    {
        return jumping;
    }

    public void setMud(bool b)
    {
        onMud = b;
    }

    public void getHit()
    {
        rb.isKinematic = true;
        gameover = true;
        health.sprite = healthMissing;
        animator.SetBool("Defeat", true);
        StartCoroutine(gameOver());
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(4);
        defeated.gameObject.SetActive(true);
    }

    public void winGame()
    {
        rb.isKinematic = true;
        victory = true;
        animator.SetBool("Victory", true);
    }

    public void setWarning()
    {
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        timer = warnTimer;
    }
}
