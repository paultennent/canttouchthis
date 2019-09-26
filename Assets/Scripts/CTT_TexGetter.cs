using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_TexGetter : MonoBehaviour
{

    private CTT_TextGen control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("Control").GetComponent<CTT_TextGen>();
        GetComponent<Renderer>().material.mainTexture = control.getTexture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
