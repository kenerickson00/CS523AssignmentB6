using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overhead_Camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      //  Vector3 playerMovement = player.transform.position - lastPosition;
      //  if(playerMovement.x - lastPosition.x > threshold || )
     //   if(player.transform.position - lastPosition > threshold)
     //   {
            transform.position = player.transform.position + offset;
            lastPosition = player.transform.position;
     //   }
    }
}
