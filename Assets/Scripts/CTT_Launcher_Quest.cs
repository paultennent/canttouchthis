using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_Launcher_Quest : MonoBehaviour
{ 
    public float power = 1000f;

    public float minAmmoRadius = 0.1f;
    public float maxAmmoRadius = 1f;

    public float pitchAngleVariationDegrees = 2f;
    public float yawAngleVariationDegrees = 2f;
    private bool readyToFire = false;

    private ParticleSystem smoke;
    ParticleSystem.EmissionModule emissionModule;

    public Animator myPirate;

    AudioSource audioSource;
    CTT_GameTracker_Quest game;
    private OvrAvatar avatar;

    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("LocalAvatar").GetComponent<OvrAvatar>();
        game = GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>();
        audioSource = GetComponent<AudioSource>();
        Vector3 p = avatar.transform.position;
        p = new Vector3(p.x, p.y - 1, p.z);
        transform.LookAt(p);
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

    public void Fire(float forceScaler)
    {
        StartCoroutine(FireProcedure(forceScaler));
    }

    private IEnumerator FireProcedure(float forceScaler)
    {
        audioSource.Play();
        SetSmokeEmmissionRate(20f);
        GameObject bullet = game.getNextBall();//Instantiate(ammo, transform.position, transform.rotation);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        float radius = Random.Range(minAmmoRadius, maxAmmoRadius);
        bullet.transform.localScale = new Vector3(radius, radius, radius);
        bullet.transform.Find("Billboard").gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_ScaleX", radius);
        bullet.transform.Find("Billboard").gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_ScaleY", radius);
        bullet.GetComponent<CTT_TTL>().activate_TTL();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power * forceScaler,ForceMode.Force);
        readyToFire = false;
        yield return new WaitForSeconds(0.25f);
        SetSmokeEmmissionRate(0f);
        
    }

    public void PrepareToFire()
    {
        myPirate.SetTrigger("Taunt");
        Vector3 p = avatar.transform.position;
        p = new Vector3(p.x, p.y - 1, p.z);
        transform.LookAt(p);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 15 + GetPitchVariation(), transform.localEulerAngles.y + GetYawVariation(), transform.localEulerAngles.z);
        readyToFire = true;
    }
}
