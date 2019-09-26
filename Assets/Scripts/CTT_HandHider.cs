using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CTT_HandHider : MonoBehaviour {

    public bool hideControllers = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         HideController();
    }

    public void HideController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                if (hideControllers)
                {
                    hand.HideController(false);
                }
                else
                {
                    hand.ShowController(false);
                }
            }
        }
    }
}
