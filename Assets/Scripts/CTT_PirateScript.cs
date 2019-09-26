using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_PirateScript : MonoBehaviour
{

    public GameObject ragdollPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            GameObject ragdoll = Instantiate(ragdollPrefab);
            ragdoll.transform.parent = GameObject.Find("BodyPit").transform;
            ragdoll.transform.localScale = transform.localScale;
            CTT_Ragdoll rs = ragdoll.GetComponent<CTT_Ragdoll>();
            rs.doCopyPosition(transform);
            rs.doHit();
            gameObject.SetActive(false);
        }
    }
}
