using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CTT_Sword : MonoBehaviour
{

    private Hand hand;
    private Rigidbody rb;
    private float HapticMultiplier = 1000f;
    private CTT_SwordSounds sounds;
    public float MaxTouchEffectDuration = 1f;

    private float timeTouching = 0f;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<Hand>();
        rb = GetComponent<Rigidbody>();
        sounds = GetComponent<CTT_SwordSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerHapticPulse(float velocity)
    {
        //hand.TriggerHapticPulse(1f, 100f, velocity + rb.velocity.magnitude * HapticMultiplier);
        StartCoroutine(LongVibration(0.1f, velocity + rb.velocity.magnitude * HapticMultiplier));
        //sounds.Play();
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            timeTouching += Time.deltaTime;
            if (timeTouching < MaxTouchEffectDuration)
            {
                hand.TriggerHapticPulse(3999);
                sounds.Play();
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            timeTouching = 0f;
        }
    }



    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
        hand.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
        yield return null;
    }
}
}
