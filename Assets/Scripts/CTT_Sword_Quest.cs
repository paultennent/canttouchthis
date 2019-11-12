using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_Sword_Quest : MonoBehaviour
{

    public Hand hand;
    private Rigidbody rb;
    private CTT_SwordSounds sounds;
    public float MaxTouchEffectDuration = 1f;

    public enum Hand
    {
        LEFT,
        RIGHT
    };

    private float timeTouching = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        sounds = GetComponent<CTT_SwordSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerHapticPulse(float velocity)
    {
        float v = velocity + rb.velocity.magnitude * 1000f;
        v = Mathf.Clamp(v, 0f, 1f);
        StartCoroutine(LongVibration(0.1f, v));
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            timeTouching += Time.deltaTime;
            if (timeTouching < MaxTouchEffectDuration)
            {
                if (hand == Hand.LEFT)
                {
                    OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
                }
                if (hand == Hand.RIGHT)
                {
                    OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                }
                sounds.Play();
            }
        }
    }


    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            if (hand == Hand.LEFT)
            {
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
            if (hand == Hand.RIGHT)
            {
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            }
            timeTouching = 0f;
        }
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
}

