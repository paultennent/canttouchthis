using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CTT_TextFloater_Quest : MonoBehaviour
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
        OvrAvatar avatar = GameObject.Find("LocalAvatar").GetComponent<OvrAvatar>();
        transform.LookAt(avatar.transform);
    }

   
}
