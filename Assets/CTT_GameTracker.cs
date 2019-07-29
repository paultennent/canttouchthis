using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CTT_GameTracker : MonoBehaviour
{

    public TextMeshPro scores;
    int hits = 0;
    int misses = 0;
    float time = 0;

    public float gameTime = 60f;

    private bool gameActive = false;
    public CTT_Launcher[] launchers;

    public bool AutoStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (AutoStart)
        {
            gameReset();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameReset();
            }
        }

        if (gameActive) {
            time = time + Time.deltaTime;
            scores.text = "Time:" + (gameTime - (int)time) + "\nHits:" + hits + "\nMisses:" + misses;
            if (time > gameTime)
            {
                gameActive = false;
                foreach (CTT_Launcher l in launchers)
                {
                    l.setLauncherActive(false);
                }
                float myScore = hits - misses;
                scores.text = "Final Score: " + myScore;
            }

        }
    }

    public void AddHit()
    {
        hits++;
    }

    public void AddMiss()
    {
        misses++;
    }

    public void gameReset()
    {
        hits = 0;
        misses = 0;
        time = 0;
        gameActive = true;
        foreach(CTT_Launcher l in launchers)
        {
            l.setLauncherActive(true);
        }
    }
}
