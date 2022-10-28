using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioClip MouseOver;
    public AudioClip MouseClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = MouseOver;
        audio.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = MouseClick;
        audio.Play();
    }
}
