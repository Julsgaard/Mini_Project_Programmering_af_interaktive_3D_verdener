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

    // Start is called before the first frame update
    void Start()
    {
        //Gets the AudioSource from the rocking chair and the second door in the hospital
        rockingChairAudioSource = rockingChair.GetComponent<AudioSource>();
        secondDoorAudioSource = secondDoor.GetComponent<AudioSource>();
    }

    //Starts animation when colliding with player, only on trigger enter
    void OnTriggerEnter(Collider other)
    {
        //Checks if the Player has entered the room
        if (other.gameObject.CompareTag("Player"))
        {
            //Only makes the if statement run once
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

    //Opens the door after 5 seconds
    IEnumerator OpenDoorAfter5Seconds()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //Opens the door and play audio
        secondDoor.GetComponent<Animator>().Play("OpenDoorAnimation");
        secondDoorAudioSource.Play();
    }
}
