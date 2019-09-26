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
            if (trackcounter == -1) {
                trackcounter = 0;
            }
            else {
                trackcounter = Random.Range(0,tracks.Length);
            }
            aud.clip = tracks[trackcounter];
            aud.Play();
        }
    }

    public void restart()
    {
        aud.Stop();
        trackcounter = Random.Range(0, tracks.Length);
    }

    public void restartMenu()
    {
        aud.Stop();
        trackcounter = -1;
    }
}
