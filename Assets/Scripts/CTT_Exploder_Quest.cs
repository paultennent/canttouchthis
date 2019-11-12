using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CTT_Exploder_Quest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject explodePartPrefab;
    public int explodeCount = 10;
    public GameObject infoPrefab;
    public bool addToScore = true;
    public bool realBall = true;

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
            if (collision.gameObject.GetComponent<CTT_Sword_Quest>() != null)
            {
                collision.gameObject.GetComponent<CTT_Sword_Quest>().triggerHapticPulse(GetComponent<Rigidbody>().velocity.magnitude);
            }
            collision.gameObject.GetComponent<CTT_SwordSounds>().PlayPop();

            doExplode();

            if (collision.gameObject.tag == "projectile")
            {
                Destroy(collision.gameObject, 1f);
            }
        }
    }

    public void doExplode()
    {
        for (int explodeIndex = 0; explodeIndex < explodeCount; explodeIndex++)
        {
            GameObject explodePart = (GameObject)GameObject.Instantiate(explodePartPrefab, this.transform.position, this.transform.rotation);
            explodePart.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
            explodePart.GetComponent<CTT_TTL>().activate_TTL();
        }
        

        if (addToScore)
        {
            if (realBall)
            {
                GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>().AddHit();
            }
            else
            {
                GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>().AddHitNotRealBall();
            }
        }

        if (GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>().enableALMessages)
        {
            GameObject floater = Instantiate(infoPrefab);
            floater.GetComponent<CTT_TTL>().activate_TTL();
            floater.transform.parent = GameObject.Find("InfoPit").transform;
            floater.transform.position = transform.position;
        }
       
        Destroy(this.gameObject);
    }
}
