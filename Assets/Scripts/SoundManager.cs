using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //The GameObjects AudioSource
    public static AudioSource audioSource;

    //Music
    public AudioClip menuMusic;
    public AudioClip firstClip;
    public AudioClip nextClip;
    public AudioClip finalClip;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the AudioSource component from the Gameobject
        audioSource = GetComponent<AudioSource>();

        //Starts the menu's music and makes it loop
        audioSource.clip = menuMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If there is no music playing then the if statement will run
        if (!audioSource.isPlaying)
        {
            //If dolls collected is less than 4 then the normal ambient music will play. Else the final ambient music will play
            if(GameManager.dolls < 4)
            {
                PlayNextClip();
            }
            else
            {
                PlayFinalClip();
            }
        }
        
        //Play the final music when 4 dolls are collected. The reason why we need this is because we want the music to play
        //right when the last doll is collected and not just after PlayNextClip is done playing
        if (audioSource.clip == nextClip && GameManager.dolls == 4)
        {
            PlayFinalClip();
        }
    }

    //Play first music clip
    public void PlayFirstClip()
    {
        audioSource.clip = firstClip;
        audioSource.Play();
        audioSource.loop = false;
    }

    //Play next music clip
    public void PlayNextClip()
    {
        audioSource.clip = nextClip;
        audioSource.Play();
    }

    //Play the final music clip
    public void PlayFinalClip()
    {
        audioSource.clip = finalClip;
        audioSource.Play();
    }
}
