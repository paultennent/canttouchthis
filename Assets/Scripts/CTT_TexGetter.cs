using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_TexGetter : MonoBehaviour
{

    public bool realBall = true;
    private CTT_TextGen control;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Control").GetComponent<CTT_TextGen>().ready && GameObject.Find("NotBallTextGen").GetComponent<CTT_TextGen>().ready && !done)
        {
            getTex();
        }
    }

    public void getTex()
    {
        done = true;
        if (realBall)
        {
            control = GameObject.Find("Control").GetComponent<CTT_TextGen>();
            GetComponent<Renderer>().material.mainTexture = control.getTexture();
        }
        else
        {
            control = GameObject.Find("NotBallTextGen").GetComponent<CTT_TextGen>();
            GetComponent<Renderer>().material.mainTexture = control.getTexture();
        }
    }
}
