using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    //AudioClips
    public AudioClip MouseOver;
    public AudioClip MouseClick;

    //Plays sound when the mouse is over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = MouseOver;
        audio.Play();
    }

    //Play sound when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = MouseClick;
        audio.Play();
    }
}
