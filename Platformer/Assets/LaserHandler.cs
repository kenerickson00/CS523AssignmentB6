using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandler : MonoBehaviour
{
    //public GameObject laser
    private bool laserActive;
    private bool currentlyWaiting;
    private GameObject[] lasers;
    void Start()
    {
        currentlyWaiting = false;
        laserActive = true;
        lasers = GameObject.FindGameObjectsWithTag("Laser");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if(!currentlyWaiting)
                StartCoroutine(toggleLaser());
    }

    private IEnumerator toggleLaser()
    {
        //Debug.Log("Currently waiting: " + currentlyWaiting);
        currentlyWaiting = true;
        yield return new WaitForSeconds(2);
        laserActive = !laserActive;
        foreach(GameObject laser in lasers)
            laser.SetActive(laserActive);
        currentlyWaiting = false;
    }
}
