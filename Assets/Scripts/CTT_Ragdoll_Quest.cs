using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_Ragdoll_Quest : MonoBehaviour
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
        OvrAvatar avatar = GameObject.Find("LocalAvatar").GetComponent<OvrAvatar>();
        head.AddForce(-(avatar.transform.position - head.transform.position) * 10f, ForceMode.Impulse);
    }
}
