using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class CTT_GameTracker : MonoBehaviour
{

    public TextMeshPro scores;
    public TextMeshPro title;
    int hits = 0;
    int misses = 0;
    float time = 0;

    public float gameTime = 60f;

    private bool gameActive = false;
    public CTT_Launcher[] launchers;

    public bool AutoStart = true;

    public float defaultTriggerTime = 2f;
    public float timeVariation = 1f;
    private float curTime;
    private CTT_Launcher curLauncher;

    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller

    public Transform ballPit;
    public GameObject ballPrefab;

    private GameObject[] balls;
    private int curBall = 0;

    public float minForceScaler = 0.1f;
    public float maxForceScaler = 1f;

    public int level = 1;
    private float levelTime = 0f;
    public float levelChangeTime = 10f;

    public int preGeneratedBalls = 25;

    // Start is called before the first frame update
    void Start()
    {
        if (grabPinch != null)
        {
            grabPinch.AddOnStateDownListener(OnTriggerPressed, inputSource);
        }
        if (AutoStart)
        {
            gameReset();
        }
    }

    private void OnTriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger Pressed");
        if (!gameActive)
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
            title.text = "Level:"+(level+1);
            scores.text = "Time:" + (gameTime - (int)time) + "\nHits:" + hits + "\nMisses:" + misses;
            if (time > gameTime)
            {
                gameActive = false;
                float myScore = hits - misses;
                title.text = "Can't Touch This";
                scores.text = "Final Score: " + myScore + "\n Trigger to Replay" ;
            }
        }

        if (gameActive)
        {
            curTime = curTime - Time.deltaTime;
            levelTime = levelTime - Time.deltaTime;
            if (curTime <= .5f && !curLauncher.isReadyToFire())
            {
                
                curLauncher.PrepareToFire();
            }

            if (curTime <= 0)
            {
                curLauncher.Fire(Random.Range(minForceScaler,maxForceScaler));
                curTime = GetNextTriggerTime();
                curLauncher = launchers[UnityEngine.Random.Range(0, launchers.Length)];
            }
            if(levelTime <= 0)
            {
                level++;
                foreach(CTT_Launcher l in launchers)
                {
                    l.pitchAngleVariationDegrees = (level) * .2f;
                    l.yawAngleVariationDegrees = (level) * .2f;
                }
                levelTime = levelChangeTime;
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
        GenerateBalls(preGeneratedBalls);
        hits = 0;
        misses = 0;
        time = 0;
        level = 0;
        gameActive = true;
        curTime = GetNextTriggerTime();
        levelTime = levelChangeTime;
        curLauncher = launchers[UnityEngine.Random.Range(0, launchers.Length)];
    }

    private float GetNextTriggerTime()
    {
        return defaultTriggerTime + UnityEngine.Random.Range(-timeVariation, +timeVariation);
    }

    private void GenerateBalls(int count)
    {
        if(balls == null)
        {
            balls = new GameObject[count];
        }
        else
        {
            foreach(GameObject ball in balls)
            {
                Destroy(ball);
            }
        }
        for(int i = 0; i < count; i++)
        {
            balls[i] = Instantiate(ballPrefab, ballPit);
        }
        curBall = 0;
    }

    public GameObject getNextBall()
    {
        GameObject ball = balls[curBall];
        curBall++;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        return ball;
    }
}
