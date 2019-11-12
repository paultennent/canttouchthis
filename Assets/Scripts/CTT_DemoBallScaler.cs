using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_DemoBallScaler : MonoBehaviour
{

    private float radius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(radius, radius, radius);
        transform.Find("Billboard").gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_ScaleX", radius);
        transform.Find("Billboard").gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_ScaleY", radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
