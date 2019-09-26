using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_PirateSounds : MonoBehaviour
{
    public AudioClip[] deathSounds;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
