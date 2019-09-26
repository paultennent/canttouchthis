using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;

public class CTT_TextFloater : MonoBehaviour
{
    // Start is called before the first frame update
    public float floatStrength = 3.5f;
    public string[] texts;

    void Start()
    {
        transform.Find("Info").GetComponent<TextMeshPro>().SetText(texts[Random.Range(0, texts.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * floatStrength);
        transform.LookAt(Player.instance.headCollider.transform);
    }

   
}
