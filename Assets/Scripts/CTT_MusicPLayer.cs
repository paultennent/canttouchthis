using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_MusicPLayer : MonoBehaviour
{

    private AudioSource aud;
    public AudioClip[] tracks;

    private int trackcounter = -1;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aud.isPlaying)
        {
            trackcounter += 1;
            if(trackcounter == tracks.Length)
            {
                trackcounter = 0;
            }
            aud.clip = tracks[trackcounter];
            aud.Play();
        }
    }

    public void restart()
    {
        trackcounter = -1;
        aud.Stop();
    }
}
