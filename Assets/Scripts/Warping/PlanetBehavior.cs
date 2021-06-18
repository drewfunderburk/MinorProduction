using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.ShaderGraph;

public class PlanetBehavior : MonoBehaviour
{
    public GameObject warpManager;

    public Color GeneratedColor;
    public Color MatchedColor;
    public Color AtmosphereColor;
    public Vector2 ColorClampMinMax;
    public float f_planetSelectMode_scale;

    public bool planetSelectMode = false;
    public bool generateOnAwake = false; 

    private float iniSize;
    public float generatedSize;

    public GameObject planet;
    public GameObject atmo;

    public bool IsDifficultPlanet;
    public bool IsEasyPlanet;

    private bool NewSizeApplied = false;
    private float PlanetSelectMode_f;

    // Start is called before the first frame update
    void Start()
    {
        iniSize = gameObject.transform.localScale.x;
        AtmosphereColor.a = 0.25f;
        generatedSize = iniSize;
        if (generateOnAwake == true) { Generate(); }
    }


    // Update is called once per frame
    void Update()
    {
        if (warpManager)
        {
            planetSelectMode = warpManager.GetComponent<WarpManager>().inPlanetSelect;
        }
        else { planetSelectMode = false;}

        if (planetSelectMode == true)
        {
            gameObject.transform.localScale = new Vector3(f_planetSelectMode_scale * generatedSize, f_planetSelectMode_scale * generatedSize, f_planetSelectMode_scale * generatedSize);
            PlanetSelectMode_f = 1f;
        }
        else 
        {
            PlanetSelectMode_f = 0f;

            if (NewSizeApplied == false)
            {
                gameObject.transform.localScale = new Vector3(generatedSize, generatedSize, generatedSize);
                NewSizeApplied = true;
            }
        }

        planet.GetComponent<Renderer>().material.SetFloat("PlanetSelectMode", PlanetSelectMode_f);
        planet.GetComponent<Renderer>().material.SetColor("OceanColor", GeneratedColor);
        planet.GetComponent<Renderer>().material.SetColor("LandColor", MatchedColor);
        planet.GetComponent<Renderer>().material.SetColor("AtmoColor", AtmosphereColor);

        atmo.GetComponent<Renderer>().material.SetColor("AtmoColor", AtmosphereColor);
    }
    void Complimentary()
    {

        float redValue;
        float greenValue;
        float blueValue;
            
        redValue = Random.Range(0, 80);
        greenValue = Random.Range(0, 80);
        blueValue = Random.Range(0, 80);

        GeneratedColor.r = redValue / 100f;
        GeneratedColor.g = greenValue / 100f;
        GeneratedColor.b = blueValue / 100f;

        MatchedColor.r = Mathf.Clamp(1 - GeneratedColor.r, ColorClampMinMax.x, ColorClampMinMax.y);
        MatchedColor.g = Mathf.Clamp(1 - GeneratedColor.g, ColorClampMinMax.x, ColorClampMinMax.y);
        MatchedColor.b = Mathf.Clamp(1 - GeneratedColor.b, ColorClampMinMax.x, ColorClampMinMax.y);

        AtmosphereColor.r = Mathf.Clamp(GeneratedColor.r + GeneratedColor.r / 4f, 0, 1) * 2f;
        AtmosphereColor.g = Mathf.Clamp(GeneratedColor.g + GeneratedColor.g / 4f, 0, 1) * 2f;
        AtmosphereColor.b = Mathf.Clamp(GeneratedColor.b + GeneratedColor.b / 4f, 0, 1) * 2f;

            ChangeSize();
    }
    void OffsetColor()
    {

        float redValue;
        float greenValue;
        float blueValue;
        
        redValue = Random.Range(0, 80);
        greenValue = Random.Range(0, 80);
        blueValue = Random.Range(0, 80);

        GeneratedColor.r = redValue / 100f;
        GeneratedColor.g = greenValue / 100f;
        GeneratedColor.b = blueValue / 100f;

        MatchedColor.r = Mathf.Clamp01(GeneratedColor.r + GeneratedColor.r/2f);
        MatchedColor.g = Mathf.Clamp01(GeneratedColor.g + GeneratedColor.g/2f);
        MatchedColor.b = Mathf.Clamp01(GeneratedColor.b + GeneratedColor.b/2f);

        AtmosphereColor.r = Mathf.Clamp(GeneratedColor.r + GeneratedColor.r / 4f, 0, 1) * 2f;
        AtmosphereColor.g = Mathf.Clamp(GeneratedColor.g + GeneratedColor.g / 4f, 0, 1) * 2f;
        AtmosphereColor.b = Mathf.Clamp(GeneratedColor.b + GeneratedColor.b / 4f, 0, 1) * 2f;
            
        ChangeSize();

    }

    void Analagous()
    {

        float degree;
        degree = Random.Range(0f,360f);

        GeneratedColor.r = Mathf.Clamp(Mathf.Sin( 1.66f * Mathf.PI * (degree / 360) + 0.53f) + 0.5f, ColorClampMinMax.x, ColorClampMinMax.y);
        GeneratedColor.g = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * (degree / 360) + 1.53f) + 0.5f, ColorClampMinMax.x, ColorClampMinMax.y);
        GeneratedColor.b = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * (degree / 360) + 2.53f) + 0.5f, ColorClampMinMax.x, ColorClampMinMax.y);

        MatchedColor.r = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree + 30f) / 360) + 0.53f) + 0.5f, ColorClampMinMax.x, ColorClampMinMax.y);
        MatchedColor.g = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree + 30f) / 360) + 1.53f) + 0.5f, ColorClampMinMax.x, ColorClampMinMax.y);
        MatchedColor.b = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree + 30f) / 360) + 2.53f) + 0.5f, ColorClampMinMax.x, ColorClampMinMax.y);

        AtmosphereColor.r = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree - 30f) / 360) + 0.53f) + 0.5f, 0, 1) * 2f;
        AtmosphereColor.g = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree - 30f) / 360) + 1.53f) + 0.5f, 0, 1) * 2f;
        AtmosphereColor.b = Mathf.Clamp(Mathf.Sin(1.66f * Mathf.PI * ((degree - 30f) / 360) + 2.53f) + 0.5f, 0, 1) * 2f;

        ChangeSize();
    }

    void ChangeSize()
    {
        if (!generateOnAwake)
        {
            generatedSize = Random.Range(1f, 1.25f);
        }
    }

    public void Generate()
    {
        NewSizeApplied = false;

        int colorSelect = Random.Range(1, 3);

        if (IsDifficultPlanet)
        {
            if (colorSelect == 1)
            {
                Complimentary();
            }

            if (colorSelect == 2)
            {
                OffsetColor();
            }

            if (colorSelect == 3)
            {
                Analagous();
            }
        }
        else 
        {
            if (colorSelect == 3)
            {
                Complimentary();
            }

            if (colorSelect == 2)
            {
                OffsetColor();
            }

            if (colorSelect == 1)
            {
                Analagous();
            }
        }

    }

}
