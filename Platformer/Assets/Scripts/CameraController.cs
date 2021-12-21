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
        float mouseX = (Input.mousePosition.x / Screen.width ) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        //XY values determine viewing angles
        transform.localRotation = Quaternion.Euler (new Vector4 (-1f * (mouseY * 45f), (mouseX * 45f), transform.localRotation.z));
    }
}
