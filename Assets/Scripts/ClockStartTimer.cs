using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class ClockStartTimer : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] Collider Trigger;

    [Header("Clock Audio Settings")]
    [SerializeField] AudioSource clockAudioSource; // Main audio source to adjust
    [SerializeField] float volumeIncrement = 0.2f; // Amount to increase volume by
    [SerializeField] float maxMainVolume = 1.0f; // Maximum main volume

    [Header("Heartbeat Audio Settings")]
    [SerializeField] AudioSource heartbeatAudioSource; // Heartbeat audio source
    [SerializeField] float heartbeatStartDelay = 120f; // Time in seconds before heartbeat starts
    [SerializeField] float heartbeatVolumeIncreaseSpeed = 0.01f; // Speed at which heartbeat volume increases
    [SerializeField] float maxHeartbeatVolume = 1.0f; // Maximum heartbeat volume

    [Header("Jumpscare")]
    [SerializeField] AudioClip jumpscare;

    [Header("Canvas")]
    [SerializeField] Canvas canvas;

    private float clockAudioTimer;
    private float heartbeatTimer;
    private bool heartbeatStarted;
    private float timer;
    private bool isStart = false;
    private void Start()
    {
        Trigger.enabled = true;
        canvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            Trigger.enabled = false;
            clockAudioSource.Play();
            isStart = true;
        }
    }

    private void Update()
    {
        if (isStart)
        {
            HandleClockVolumeIncrease();
            HandleHeartbeatStartAndVolumeIncrease();
        }
    }

    private void HandleClockVolumeIncrease()
    {
        clockAudioTimer += Time.deltaTime;

        if (clockAudioTimer >= 30f) // Every 30 seconds
        {
            clockAudioTimer = 0f; // Reset the timer
            clockAudioSource.volume = Mathf.Clamp(clockAudioSource.volume + volumeIncrement, 0f, maxMainVolume);
            Debug.Log($"Main audio volume increased to: {clockAudioSource.volume}");
        }
    }

    private void HandleHeartbeatStartAndVolumeIncrease()
    {
        heartbeatTimer += Time.deltaTime;

        if (!heartbeatStarted && heartbeatTimer >= heartbeatStartDelay)
        {
            heartbeatAudioSource.Play(); // Start the heartbeat audio
            heartbeatStarted = true;
            Debug.Log("Heartbeat audio started!");
        }

        if (heartbeatStarted && heartbeatAudioSource.volume < maxHeartbeatVolume)
        {
            // Gradually increase the heartbeat volume
            heartbeatAudioSource.volume = Mathf.Clamp(
                heartbeatAudioSource.volume + (heartbeatVolumeIncreaseSpeed * Time.deltaTime),
                0f,
                maxHeartbeatVolume
            );
            Debug.Log($"Heartbeat volume increased to: {heartbeatAudioSource.volume}");
        }

        if (heartbeatTimer >= 180.0f)
        {
            heartbeatAudioSource.volume = 1;
        }

        if (heartbeatTimer >= 240.0f)
        {
            heartbeatAudioSource.clip = jumpscare;
            heartbeatAudioSource.volume = 0.5f;
            heartbeatAudioSource.Play();
            Time.timeScale = 0;
            canvas.enabled = true;
        }
    }
}
