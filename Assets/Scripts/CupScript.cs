using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    [Header("Object Requirements")]
    [SerializeField] private string requiredTag; // Tag of the correct object
    [SerializeField] private string requiredName; // Name of the correct object

    [Header("Lock Settings")]
    [SerializeField] private GameObject lockToUnlock; // Reference to the lock to unlock

    private bool isUnlocked = false; // Ensure the lock is unlocked only once

    private void OnTriggerEnter(Collider other)
    {
        if (isUnlocked) return; // Skip if already unlocked

        // Check if the object's tag and name match the requirements
        if (other.CompareTag(requiredTag) && other.name == requiredName)
        {
            Debug.Log("Correct object placed on the cup!");
        }
        else
        {
            Debug.Log($"Incorrect object. Expected tag: {requiredTag}, name: {requiredName}");
        }
    }
}
