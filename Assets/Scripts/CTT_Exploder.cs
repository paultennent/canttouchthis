using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CTT_Exploder : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject explodePartPrefab;
    public int explodeCount = 10;
    public GameObject infoPrefab;
    public bool addToScore = true;

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
            for (int explodeIndex = 0; explodeIndex < explodeCount; explodeIndex++)
            {
                GameObject explodePart = (GameObject)GameObject.Instantiate(explodePartPrefab, this.transform.position, this.transform.rotation);
                explodePart.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                explodePart.GetComponent<CTT_TTL>().activate_TTL();
            }
            if (collision.gameObject.GetComponent<CTT_Sword>() != null)
            {
                collision.gameObject.GetComponent<CTT_Sword>().triggerHapticPulse(GetComponent<Rigidbody>().velocity.magnitude);
            }
            collision.gameObject.GetComponent<CTT_SwordSounds>().PlayPop();
            if (addToScore)
            {
                GameObject.Find("Control").GetComponent<CTT_GameTracker>().AddHit();
            }

            if (GameObject.Find("Control").GetComponent<CTT_GameTracker>().enableALMessages)
            {
                GameObject floater = Instantiate(infoPrefab);
                floater.GetComponent<CTT_TTL>().activate_TTL();
                floater.transform.parent = GameObject.Find("InfoPit").transform;
                floater.transform.position = transform.position;
            }
            if(collision.gameObject.tag == "projectile")
            {
                Destroy(collision.gameObject,1f);
            }
            Destroy(this.gameObject);
        }
    }
}
