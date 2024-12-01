using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorTransform; // The transform of the door
    [SerializeField] private float movementThreshold = 0.01f; // Minimum distance or rotation change to detect movement

    [Header("Audio Settings")]
    [SerializeField] private AudioSource doorAudioSource; // AudioSource component for the door sound
    [SerializeField] private AudioClip[] doorSounds; // Array to hold multiple door sound clips

    private Vector3 lastPosition; // Last position of the door
    private Quaternion lastRotation; // Last rotation of the door

    private void Start()
    {
        if (doorTransform == null)
        {
            doorTransform = transform; // Default to the current object's transform if none is set
        }

        if (doorAudioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }

        if (doorSounds == null || doorSounds.Length == 0)
        {
            Debug.LogError("No door sounds assigned!");
        }

        // Initialize the position and rotation
        lastPosition = doorTransform.position;
        lastRotation = doorTransform.rotation;
    }

    private void Update()
    {
        if (doorTransform == null || doorAudioSource == null || doorSounds == null || doorSounds.Length == 0) return;

        // Check for movement or rotation
        float positionChange = Vector3.Distance(doorTransform.position, lastPosition);
        float rotationChange = Quaternion.Angle(doorTransform.rotation, lastRotation);

        if (positionChange > movementThreshold || rotationChange > movementThreshold)
        {
            PlayRandomDoorSound();

            // Update last position and rotation
            lastPosition = doorTransform.position;
            lastRotation = doorTransform.rotation;
        }
    }

    private void PlayRandomDoorSound()
    {
        if (!doorAudioSource.isPlaying) // Avoid overlapping sounds
        {
            int randomIndex = Random.Range(0, doorSounds.Length); // Choose a random clip
            doorAudioSource.clip = doorSounds[randomIndex];
            doorAudioSource.Play();
        }
    }
}
