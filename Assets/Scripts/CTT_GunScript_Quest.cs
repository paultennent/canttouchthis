using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_GunScript_Quest : MonoBehaviour
{

    public Hand hand;
    public OVRInput.Button cock;
    public OVRInput.Button trigger;

    public GameObject bulletPrefab;
    private Transform bulletPos;

    public float power = 500f;

    private float HapticMultiplier = 1000f;

    private ParticleSystem smoke;
    ParticleSystem.EmissionModule emissionModule;
    AudioSource audioSource;
    public CTT_PistolFireAnimation pf;

    public AudioSource popper;

    public enum Hand
    {
        LEFT,
        RIGHT
    };

    // Start is called before the first frame update
    void Start()
    {
        
        bulletPos = transform.Find("BulletPos");
        smoke = transform.Find("WhiteSmoke").GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        emissionModule = smoke.emission;
        SetSmokeEmmissionRate(0f);
    }

   

    private void cockPressed()
    {
        if (!pf.cocked && !pf.cocking)
        {
            pf.cock();
        }
    }

    private void triggerPressed()
    {
        if (GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>().isGameActive())
        {
            if (pf.cocked & !pf.cocking)
            {
                StartCoroutine(FireProcedure());
            }
        }
        else
        {
            Debug.Log("Didn't fire");
        }
    }

    private IEnumerator FireProcedure()
    {
        pf.fire();
        triggerHapticPulse(.15f,1f);
        audioSource.Play();
        yield return new WaitForSeconds(0.05f);
        SetSmokeEmmissionRate(20f);
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.parent = GameObject.Find("BulletPit").transform;
        bullet.transform.position = bulletPos.position;
        bullet.transform.rotation = bulletPos.rotation;
        bullet.GetComponent<CTT_TTL>().activate_TTL();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power, ForceMode.Force);
        yield return new WaitForSeconds(0.15f);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            CTT_Exploder_Quest eq = hit.collider.gameObject.GetComponent<CTT_Exploder_Quest>();
            if(eq != null)
            {
                eq.doExplode();
                popper.Play();
                Destroy(bullet);
            }
            CTT_PirateScript_Quest pq = hit.collider.gameObject.GetComponent<CTT_PirateScript_Quest>();
            if(pq != null)
            {
                pq.doKill();
                Destroy(bullet);
            }
        }

        SetSmokeEmmissionRate(0f);

    }

    public void triggerHapticPulse(float length, float strength)
    {
        StartCoroutine(LongVibration(length, strength));
    }

    IEnumerator LongVibration(float length, float strength)
    {
        if (hand == Hand.LEFT)
        {
            OVRInput.SetControllerVibration(strength, strength, OVRInput.Controller.LTouch);
        }
        if (hand == Hand.RIGHT)
        {
            OVRInput.SetControllerVibration(strength, strength, OVRInput.Controller.RTouch);
        }

        yield return new WaitForSeconds(length);

        if (hand == Hand.LEFT)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        if (hand == Hand.RIGHT)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(cock))
        {
            cockPressed();
        }
        if (OVRInput.GetDown(trigger))
        {
            triggerPressed();
        }
    }

    private void SetSmokeEmmissionRate(float rate)
    {
        ParticleSystem.MinMaxCurve tempCurve = emissionModule.rateOverTime;
        tempCurve.constant = rate;
        emissionModule.rateOverTime = tempCurve;
    }
}
