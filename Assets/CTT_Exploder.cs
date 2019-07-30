using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_Exploder : MonoBehaviour
{
    // Start is called before the first frame update

    public float minMagnitudeToExplode = 1f;
    public GameObject explodePartPrefab;
    public int explodeCount = 10;

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
            if (collision.impulse.magnitude > minMagnitudeToExplode)
            {
                for (int explodeIndex = 0; explodeIndex < explodeCount; explodeIndex++)
                {
                    GameObject explodePart = (GameObject)GameObject.Instantiate(explodePartPrefab, this.transform.position, this.transform.rotation);
                    explodePart.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                    explodePart.GetComponent<CTT_TTL>().activate_TTL();
                }
                collision.gameObject.GetComponent<CTT_Sword>().triggerHapticPulse(GetComponent<Rigidbody>().velocity.magnitude);
                if (addToScore)
                {
                    GameObject.Find("Control").GetComponent<CTT_GameTracker>().AddHit();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
