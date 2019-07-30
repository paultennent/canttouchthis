using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_SwordSounds : MonoBehaviour
{

    public AudioClip[] swordSounds;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public AudioClip GetRandom()
    {
        return swordSounds[Random.Range(0, swordSounds.Length)];
    }

    public void Play()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandom();
            audioSource.Play();
        }
    }


}
