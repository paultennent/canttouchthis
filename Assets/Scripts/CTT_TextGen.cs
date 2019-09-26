using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTT_TextGen : MonoBehaviour
{
    Texture2D texture;
    int[,] noise;

    public int size = 200;

    bool ready = false;

    // Start is called before the first frame update
    void Awake()
    {
        noise = new int[size, size];
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                noise[x, y] = (int)Mathf.Round(Random.value);

       
        
    }


    void Start()
    {

        texture = new Texture2D((int)size, (int)size, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
        //GetComponent<Renderer>().material.mainTexture = texture;
        generateTexture(size);
        ready = true;

    }

    public float freq = 0.02f;

    public int noiseScale = 1;

    // LM component, K
    public float la = 0.2f; // left oblique contrast
    public float lb = 0.2f; // right oblique contrast

    float theta_a = 0.25f * Mathf.PI; // / 2.0f;  //-1.0f * Mathf.PI / 2.0f; // orientation of left oblique, -45 deg clockwise from vertical
    float theta_b = -0.25f * Mathf.PI;  //; // orientation of right oblique, 45 deg clockwise from vertical
    float theta_c = 0.25f * Mathf.PI;
    float theta_d = -0.25f * Mathf.PI;

    public float phi_a = 0.5f; // spatial phase of left oblique
    public float phi_b = -0.5f; // spatial phase of right oblique
    public float phi_c = 0.5f;
    public float phi_d = 0.5f;

    public float mc = 0.2f; //modulation depths of the left and right obliques respective- ly
    public float md = 0.2f; //modulation depths of the left and right obliques respective - ly

    public float l0 = 0.5f; // mean luminance of the monitor
    public float n = 0.1f; // contrast of noise texture

    private void OnValidate()
    {
        if (ready)
        {
            generateTexture(size);
        }
    }


    // want period to be 200
    // natural period is 2 pi
    // 0.005 == 1/200 (*2pi)

    void generateTexture(int size)
    {

        float min = 10.0f;
        float max = -10.0f;

        int centre = size / 2;

        float _phi_a = Mathf.PI * phi_a; // spatial phase of left oblique
        float _phi_b = Mathf.PI * phi_b; // spatial phase of right oblique  
        float _phi_c = Mathf.PI * phi_c;  
        float _phi_d = Mathf.PI * phi_d;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float k = (la * Mathf.Cos(2.0f * Mathf.PI * freq * ((Mathf.Cos(theta_a) * x) - (Mathf.Sin(theta_a) * y)) - phi_a))
                    + (lb * Mathf.Cos(2.0f * Mathf.PI * freq * ((Mathf.Cos(theta_b) * x) - (Mathf.Sin(theta_b) * y)) - phi_b));

                float m = (mc * Mathf.Cos(2.0f * Mathf.PI * freq * ((Mathf.Cos(theta_c) * x) - (Mathf.Sin(theta_c) * y)) - phi_c))
                    + (md * Mathf.Cos(2.0f * Mathf.PI * freq * ((Mathf.Cos(theta_d) * x) - (Mathf.Sin(theta_d) * y)) - phi_d));

                float r = noise[x / noiseScale, y / noiseScale]; //.Round(Random.value);

                float l = l0 * (1.0f + k + (n * r) + (n * r * m));

                float a = 1.0f;


                int distance = (int) Mathf.Sqrt(((x - centre) * (x - centre)) + ((y - centre) * (y - centre)));
                if (distance >= centre)
                    a = 0.0f;

                texture.SetPixel(x, y, new Color(l, l, l, a));

                if (l > max)
                    max = l;
                if (l < min)
                    min = l;
            }
        }

        //Debug.Log(max + " " + min);
        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D getTexture()
    {
        return texture;
    }
}
