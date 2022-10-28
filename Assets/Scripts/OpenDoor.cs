using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject firstDoll;

    // Update is called once per frame
    void Update()
    {
        if (firstDoll == null)
        {
            GetComponent<Animator>().Play("OpenDoorAnimation");
            GetComponent<Animation>().Play("LightFlicker");
        }
    }
}
