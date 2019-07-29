using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_TTL : MonoBehaviour
{

    public float timeToLive = 5f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToLive = timeToLive - Time.deltaTime;
        if (timeToLive <= 0)
        {
            
            Destroy(gameObject, 0);
        }
    }
}
