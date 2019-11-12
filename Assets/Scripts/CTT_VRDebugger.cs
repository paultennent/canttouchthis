using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTT_VRDebugger : MonoBehaviour
{

    private Text text;
    public bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            visible = true;
        }

        if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            visible = false;
        }

        if (visible)
        {
            text.text = "FPS:" + (int)(1f / Time.unscaledDeltaTime);
        }
        else
        {
            text.text = "";
        }
        
    }
}
