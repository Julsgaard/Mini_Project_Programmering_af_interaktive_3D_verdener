using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingChairTrigger : MonoBehaviour
{
    public GameObject rockingChair;
    public GameObject secondDoor;


    //Starts animation when colliding with player
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rockingChair.GetComponent<Animator>().Play("RockingChair");
            secondDoor.GetComponent<Animator>().Play("OpenDoorAnimation");
        }

    }

}
