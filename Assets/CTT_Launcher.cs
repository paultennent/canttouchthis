﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CTT_Launcher : MonoBehaviour
{ 
    public GameObject ammo;
   
    public float power = 1000f;

    public float minAmmoRadius = 0.1f;
    public float maxAmmoRadius = 1f;

    public float pitchAngleVariationDegrees = 2f;
    public float yawAngleVariationDegrees = 2f;
    private bool readyToFire = false;

    private ParticleSystem smoke;
    ParticleSystem.EmissionModule emissionModule;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.LookAt(Player.instance.feetPositionGuess);
        smoke = transform.Find("WhiteSmoke").GetComponent<ParticleSystem>();
        emissionModule = smoke.emission;
        SetSmokeEmmissionRate(0f);
    }

    private void SetSmokeEmmissionRate(float rate)
    {
        ParticleSystem.MinMaxCurve tempCurve = emissionModule.rateOverTime;
        tempCurve.constant = rate;
        emissionModule.rateOverTime = tempCurve;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public bool isReadyToFire()
    {
        return readyToFire;
    }

    private float GetPitchVariation()
    {
        return Random.Range(-pitchAngleVariationDegrees, +pitchAngleVariationDegrees);
    }

    private float GetYawVariation()
    {
        return Random.Range(-yawAngleVariationDegrees, +yawAngleVariationDegrees);
    }

    public void Fire()
    {
        StartCoroutine(FireProcedure());
    }

    private IEnumerator FireProcedure()
    {
        audioSource.Play();
        SetSmokeEmmissionRate(20f);
        GameObject bullet = Instantiate(ammo, transform.position, transform.rotation);
        float radius = Random.Range(minAmmoRadius, maxAmmoRadius);
        bullet.transform.localScale = new Vector3(radius, radius, radius);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power,ForceMode.Force);
        readyToFire = false;
        yield return new WaitForSeconds(0.25f);
        SetSmokeEmmissionRate(0f);
    }

    public void PrepareToFire()
    {
        transform.LookAt(Player.instance.feetPositionGuess);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 15 + GetPitchVariation(), transform.localEulerAngles.y + GetYawVariation(), transform.localEulerAngles.z);
        readyToFire = true;
    }
}
