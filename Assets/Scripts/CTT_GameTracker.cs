using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;
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
    private bool gameOver = false;

    public CTT_MusicPLayer music;
    public bool enableALMessages = true;

    private float originalTriggerTime;
    private CTT_HandHider handHider;

    public CTT_WeaponSwap leftWeapon;
    public CTT_WeaponSwap rightWeapon;

    // Start is called before the first frame update
    void Start()
    {
        handHider = GetComponent<CTT_HandHider>();
        originalTriggerTime = defaultTriggerTime;
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
        if (!gameActive)
        {
            gameReset();
        }
    }

    public bool isGameActive()
    {
        return !(time < 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
            if (time > gameTime || gameOver)
            {
                gameActive = false;
                float myScore = hits - misses;
                title.text = "Can't Touch This";
                scores.text = "Final Score: " + myScore + "\nPress Menu to Replay" ;
                killAllBalls();
                handHider.hideControllers = false;
                leftWeapon.switchWeapon(CTT_WeaponSwap.Weapon.None);
                rightWeapon.switchWeapon(CTT_WeaponSwap.Weapon.None);
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
                curLauncher = getNextLauncher();//launchers[UnityEngine.Random.Range(0, launchers.Length)];
                

            }
            if(levelTime <= 0)
            {
                level++;
                foreach(CTT_Launcher l in launchers)
                {
                    l.pitchAngleVariationDegrees = (level) * .2f;
                    l.yawAngleVariationDegrees = (level) * .2f;
                }
                defaultTriggerTime *= .9f;
                levelTime = levelChangeTime;
            }
        }

        




    }

    private CTT_Launcher getNextLauncher()
    {
        //only fire from launchers with living pirates
        //first check they're not all dead
        List<CTT_Launcher> myList = new List<CTT_Launcher>();
        foreach(CTT_Launcher l in launchers)
        {
            if (l.myPirate.isActiveAndEnabled)
            {
                myList.Add(l);
            }
        }
        if(myList.Count == 0)
        {
            gameOver = true;
            return null;
        }
        else
        {
            int choice = Random.Range(0, myList.Count);
            return myList[choice];
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
        //GenerateBalls(preGeneratedBalls);
        hits = 0;
        misses = 0;
        time = 0;
        level = 0;
        gameActive = true;
        curTime = GetNextTriggerTime();
        levelTime = levelChangeTime;
        curLauncher = launchers[UnityEngine.Random.Range(0, launchers.Length)];
        gameOver = false;
        music.restart();
        defaultTriggerTime = originalTriggerTime;
        handHider.hideControllers = true;
        leftWeapon.switchWeapon(leftWeapon.startWeapon);
        rightWeapon.switchWeapon(rightWeapon.startWeapon);

        //clear bodies
        foreach (Transform t in GameObject.Find("BodyPit").transform)
        {
            Destroy(t.gameObject);
        }
        //revive pirates
        foreach (Transform t in GameObject.Find("Pirates").transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    private float GetNextTriggerTime()
    {
        return defaultTriggerTime + UnityEngine.Random.Range(-timeVariation, +timeVariation);
    }

    

    public GameObject getNextBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballPit);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        return ball;
    }

    private void  killAllBalls()
    {
        foreach(Transform t in ballPit)
        {
            Destroy(t.gameObject);
        }
    }
}
