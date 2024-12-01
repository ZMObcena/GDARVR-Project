using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    [Header("Object Requirements")]
    [SerializeField] string requiredTag; 
    [SerializeField] string requiredName;

    [Header("Barricade")]
    [SerializeField] GameObject barricade;

    [Header("Audio Clips")]
    [SerializeField] AudioSource audioSource; 
    [SerializeField] AudioClip ding;
    [SerializeField] AudioClip screech;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag) && other.name == requiredName)
        {
            audioSource.clip = ding;
            audioSource.Play();
            barricade.SetActive(false);
        }
        else
        {
            audioSource.clip = screech;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag) && other.name == requiredName)
        {
            barricade.SetActive(true);
        }
    }
}
