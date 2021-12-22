using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform agent;
    public Transform body;
    public Transform cam;
    public float rotspeed;

    private Vector3 last;
    private Vector3 alast;
    // Start is called before the first frame update
    void Start()
    {
        last = transform.TransformPoint(body.position);
        alast = transform.TransformPoint(agent.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.TransformPoint(body.position) - last; //- (transform.TransformPoint(agent.position) - alast);
        last = transform.TransformPoint(body.position);
        alast = transform.TransformPoint(agent.position);

        bool leftrot = Input.GetKey(KeyCode.LeftShift);
        bool rightrot = Input.GetKey(KeyCode.RightShift);/**
        if(leftrot)
        {
            if(!rightrot)
            {
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y - rotspeed, cam.transform.eulerAngles.z);
            }
        } else if(rightrot)
        {
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y + rotspeed, cam.transform.eulerAngles.z);
        }*/
        //float mouseX = (Input.mousePosition.x / Screen.width ) - 0.5f;
        //float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        //XY values determine viewing angles
        //transform.localRotation = Quaternion.Euler (new Vector4 (transform.localRotation.x, (mouseX * 75f), transform.localRotation.z));
    }
}
