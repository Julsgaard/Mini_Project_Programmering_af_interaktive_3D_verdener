using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingChairTrigger : MonoBehaviour
{
    public GameObject rockingChair;
    public GameObject secondDoor;

    AudioSource rockingChairAudioSource;
    AudioSource secondDoorAudioSource;

    int playOnce = 0;



    void Start()
    {
        rockingChairAudioSource = rockingChair.GetComponent<AudioSource>();
        secondDoorAudioSource = secondDoor.GetComponent<AudioSource>();
    }


    //Starts animation when colliding with player
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playOnce++;
            if(playOnce == 1)
            {
                //Rocking Chair animation
                rockingChair.GetComponent<Animator>().Play("RockingChair");

                rockingChairAudioSource.Play();

                //Start 5 seconds coroutine
                StartCoroutine(OpenDoorAfter5Seconds());
            }
        }
    }

    IEnumerator OpenDoorAfter5Seconds()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //Opens the door and play audio
        secondDoor.GetComponent<Animator>().Play("OpenDoorAnimation");

        secondDoorAudioSource.Play();

    }

}
