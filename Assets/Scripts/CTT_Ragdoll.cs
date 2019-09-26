using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CTT_Ragdoll : MonoBehaviour
{

    public Rigidbody head;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doCopyPosition(Transform them)
    {
        copyPosition(them, transform);
    }

    public void copyPosition(Transform them, Transform me)
    {
        me.position = them.position;
        me.rotation = them.rotation;
        foreach (Transform t in them)
        {
            copyPosition(t,me.Find(t.name));
            
        }
        
    }

    public void doHit()
    {
        head.AddForce(-(Player.instance.headCollider.transform.position - head.transform.position) * 10f, ForceMode.Impulse);
    }
}
