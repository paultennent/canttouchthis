using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_TTL : MonoBehaviour
{

    public float timeToLive = 5f;
    bool TTL_Active = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void activate_TTL()
    {
        TTL_Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TTL_Active)
        {
            timeToLive = timeToLive - Time.deltaTime;
            if (timeToLive <= 0)
            {

                Destroy(gameObject, 0);
            }
        }
    }
}
