using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [Header("Chest Settings")]
    [SerializeField] private Transform chestTransform; // The transform of the chest
    [SerializeField] private float movementThreshold = 0.01f; // Minimum movement or rotation change to detect

    [Header("Audio Settings")]
    [SerializeField] private AudioSource chestAudioSource; // AudioSource component for the chest
    [SerializeField] private AudioClip[] chestSounds; // Array of audio clips to randomly play

    private Vector3 lastPosition; // Last recorded position of the chest
    private Quaternion lastRotation; // Last recorded rotation of the chest

    private void Start()
    {
        if (chestTransform == null)
        {
            chestTransform = transform; // Use this object's transform if none assigned
        }

        if (chestAudioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }

        if (chestSounds == null || chestSounds.Length == 0)
        {
            Debug.LogError("No chest sounds assigned!");
        }

        // Initialize position and rotation
        lastPosition = chestTransform.position;
        lastRotation = chestTransform.rotation;
    }

    private void Update()
    {
        if (chestTransform == null || chestAudioSource == null || chestSounds == null || chestSounds.Length == 0) return;

        // Detect movement or rotation
        float positionChange = Vector3.Distance(chestTransform.position, lastPosition);
        float rotationChange = Quaternion.Angle(chestTransform.rotation, lastRotation);

        if (positionChange > movementThreshold || rotationChange > movementThreshold)
        {
            PlayRandomChestSound();

            // Update last known position and rotation
            lastPosition = chestTransform.position;
            lastRotation = chestTransform.rotation;
        }
    }

    private void PlayRandomChestSound()
    {
        if (!chestAudioSource.isPlaying) // Prevent overlapping sounds
        {
            int randomIndex = Random.Range(0, chestSounds.Length); // Pick a random sound
            chestAudioSource.clip = chestSounds[randomIndex];
            chestAudioSource.Play();
        }
    }
}
