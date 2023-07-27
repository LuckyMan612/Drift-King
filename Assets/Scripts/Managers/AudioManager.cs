using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip collectSound;

    private AudioSource audioSource;

    public static AudioManager Instance;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume = 1f, Vector3 position = default)
    {       
        if(position == default)
        {
            //if no position is specified play the sound in 2D
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            //if a position is specified play the sound in 3D
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}
