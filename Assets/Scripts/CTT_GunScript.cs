using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CTT_GunScript : MonoBehaviour
{

    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller

    public GameObject bulletPrefab;
    private Transform bulletPos;

    public float power = 500f;
    private Hand hand;

    private float HapticMultiplier = 1000f;

    private ParticleSystem smoke;
    ParticleSystem.EmissionModule emissionModule;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (grabPinch != null)
        {
            grabPinch.AddOnStateDownListener(OnTriggerPressed, inputSource);
        }
        bulletPos = transform.Find("BulletPos");
        hand = GetComponentInParent<Hand>();
        smoke = transform.Find("WhiteSmoke").GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        emissionModule = smoke.emission;
        SetSmokeEmmissionRate(0f);
    }

    private void OnTriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (GameObject.Find("Control").GetComponent<CTT_GameTracker>().isGameActive())
        {
            StartCoroutine(FireProcedure());
        }
        else
        {
            Debug.Log("Didn't fire");
        }
    }

    private IEnumerator FireProcedure()
    {
        triggerHapticPulse(10);
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        SetSmokeEmmissionRate(20f);
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.parent = GameObject.Find("BulletPit").transform;
        bullet.transform.position = bulletPos.position;
        bullet.transform.rotation = bulletPos.rotation;
        bullet.GetComponent<CTT_TTL>().activate_TTL();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power, ForceMode.Force);
        yield return new WaitForSeconds(0.25f);
        SetSmokeEmmissionRate(0f);

    }

    public void triggerHapticPulse(float velocity)
    {
        //hand.TriggerHapticPulse(1f, 100f, velocity + rb.velocity.magnitude * HapticMultiplier);
        StartCoroutine(LongVibration(.15f, velocity * HapticMultiplier));
        //sounds.Play();
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            hand.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetSmokeEmmissionRate(float rate)
    {
        ParticleSystem.MinMaxCurve tempCurve = emissionModule.rateOverTime;
        tempCurve.constant = rate;
        emissionModule.rateOverTime = tempCurve;
    }
}
