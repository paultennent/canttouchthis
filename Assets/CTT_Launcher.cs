using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CTT_Launcher : MonoBehaviour
{ 
    public GameObject ammo;
    public float defaultTriggerTime = 2f;
    public float timeVariation = 1f;
    public float power = 1000f;

    public float minAmmoRadius = 0.1f;
    public float maxAmmoRadius = 1f;

    private float curTime;
    private bool launcherActive = false;

    public bool activateOnStart = true;
    public float pitchAngleVariationDegrees = 2f;
    public float yawAngleVariationDegrees = 2f;
    private bool readyToFire = false;

    // Start is called before the first frame update
    void Start()
    {
        if (activateOnStart)
        {
            setLauncherActive(true);
        }
    }

    public void setLauncherActive(bool ac)
    {
        launcherActive = ac;
        curTime = GetNextTriggerTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (launcherActive)
        {
            curTime = curTime - Time.deltaTime;
            if (curTime <= .5f && !readyToFire)
            {
                PrepareToFire();
            }

            if (curTime <= 0)
            {
                Fire();
                curTime = GetNextTriggerTime();
            }
        }
    }

    private float GetNextTriggerTime()
    {
        return defaultTriggerTime + Random.Range(-timeVariation, +timeVariation);
    }

    private float GetPitchVariation()
    {
        return Random.Range(-pitchAngleVariationDegrees, +pitchAngleVariationDegrees);
    }

    private float GetYawVariation()
    {
        return Random.Range(-yawAngleVariationDegrees, +yawAngleVariationDegrees);
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(ammo, transform.position, transform.rotation);
        float radius = Random.RandomRange(minAmmoRadius, maxAmmoRadius);
        bullet.transform.localScale = new Vector3(radius, radius, radius);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power,ForceMode.Force);
        readyToFire = false;
    }

    private void PrepareToFire()
    {
        transform.LookAt(Player.instance.feetPositionGuess);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 15 + GetPitchVariation(), transform.localEulerAngles.y + GetYawVariation(), transform.localEulerAngles.z);
        readyToFire = true;
    }
}
