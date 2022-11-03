using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    
    public static AudioSource audioSource;

    //Music
    public AudioClip menuMusic;
    //public AudioClip ambientMusic;
    //public AudioClip heathBeat;
    //public AudioClip[] ambientMusic;
    public AudioClip firstClip;
    public AudioClip nextClip;
    public AudioClip finalClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = menuMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if(GameManager.dolls < 4)
            {
                PlayNextClip();
            }
            else
            {
                PlayFinalClip();
            }
        }
        if (audioSource.clip == nextClip && GameManager.dolls == 4)
        {
            PlayFinalClip();
        }
    }

    public void PlayFirstClip()
    {
        audioSource.clip = firstClip;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void PlayNextClip()
    {
        audioSource.clip = nextClip;
        audioSource.Play();
        //audioSource.loop = false;
        //Invoke("PlayNextSong", audioSource.clip.length);
    }

    public void PlayFinalClip()
    {
        audioSource.clip = finalClip;
        audioSource.Play();
        //audioSource.loop = false;
    }

    /*public void PlayAmbientMusic()
    {
        audioSource.clip = ambientMusic;
        audioSource.Play();
        audioSource.loop = true;
    }*/

}
