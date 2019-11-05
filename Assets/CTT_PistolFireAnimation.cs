using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_PistolFireAnimation : MonoBehaviour
{
    public Transform pivot;

    private float pivotYCocked = -45;
    private float pivotYFired = 35;

    private float cockTime = 0.5f;
    private float firetime = 0.025f;

    private float curCockPos;

    public bool cocked = false;
    private AudioSource audio;
    public bool cocking = false;

    // Start is called before the first frame update
    void Start()
    {
        curCockPos = pivotYFired;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            cock();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            fire();
        }
    }

    public void cock()
    {
        StartCoroutine(cockCoroutine());
    }

    public void fire()
    {
        StartCoroutine(fireCoroutine());
    }

    private IEnumerator cockCoroutine()
    {
        cocking = true;
        audio.Play();
        float currentPos = curCockPos;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / cockTime;
            curCockPos = Mathf.Lerp(curCockPos, pivotYCocked, t);
            pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x, curCockPos, pivot.transform.localEulerAngles.z);
            yield return null;
        }
        cocked = true;
        cocking = false;
    }

    private IEnumerator fireCoroutine()
    {
        cocking = true;
        float currentPos = curCockPos;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / firetime;
            curCockPos = Mathf.Lerp(curCockPos, pivotYFired, t);
            pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x, curCockPos, pivot.transform.localEulerAngles.z);
            yield return null;
        }
        cocked = false;
        cocking = false;
    }
}
