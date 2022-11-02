using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    
    public static AudioSource audioSource;

    //Music
    public AudioClip menuMusic;
    public AudioClip ambientMusic;


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
        
    }


    public void PlayAmbientMusic()
    {
        audioSource.clip = ambientMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

}
