using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the AudioSource from the CutsceneManager GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //When the jumpscare sound is done playing then the level is changed to Scene1
        if (!audioSource.isPlaying)
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
