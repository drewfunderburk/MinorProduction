using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public Vector3 farPos;
    public Vector3 nearPos;
    public float farScale;
    public float nearScale = 2f;
    //[Range(0, 1)] public float travelTime;
    public Vector3 startPos;
    public Vector3 endPos;
    [Range(0, 1)] public float planetPosition;

    public bool levelStatus = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void planetEnRoute(float travelTime)
    {
        gameObject.transform.position = new Vector3(Mathf.SmoothStep(farPos.x, nearPos.x, travelTime), Mathf.SmoothStep(farPos.y, nearPos.y, travelTime), Mathf.SmoothStep(farPos.z, nearPos.z, travelTime));
        gameObject.transform.localScale = new Vector3(Mathf.SmoothStep(farScale, nearScale, travelTime), Mathf.SmoothStep(farScale, nearScale, travelTime), Mathf.SmoothStep(farScale, nearScale, travelTime));
    }

    public void planetActive(float levelTime)
    {
        gameObject.transform.position = new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, levelTime), Mathf.SmoothStep(startPos.y, endPos.y, levelTime), Mathf.SmoothStep(startPos.z, endPos.z, levelTime));
    }

}
