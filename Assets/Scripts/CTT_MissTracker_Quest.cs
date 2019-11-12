﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_MissTracker_Quest : MonoBehaviour
{ 
    private bool adddedToScore = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -2f)
        {
            if (!adddedToScore)
            {
                GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>().AddMiss();
                adddedToScore = true;
            }
        }
    }
}
