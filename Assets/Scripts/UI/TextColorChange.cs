using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextColorChange : MonoBehaviour
{
    [ColorUsage(true,true)]
    public Color iniColor;
    [ColorUsage(true, true)]
    public Color MouseOverColor;


    // Start is called before the first frame update
    void Start()
    {
        iniColor = GetComponent<MeshRenderer>().material.GetColor("Color_");
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void mouseOver()
    {

        GetComponent<MeshRenderer>().material.SetColor("Color_", MouseOverColor);
    }

    public void mouseExit()
    {

        GetComponent<MeshRenderer>().material.SetColor("Color_", iniColor);
    }

}
