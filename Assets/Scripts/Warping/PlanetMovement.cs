using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public Vector3 farPos;
    [Range(0, 1)] public float travelTime;
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
        if (levelStatus == true)
        {
            gameObject.transform.position = new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, planetPosition), Mathf.SmoothStep(startPos.y, endPos.y, planetPosition), Mathf.SmoothStep(startPos.z, endPos.z, planetPosition));
        }
        else { planetEnRoute(); }
    }

    public void planetEnRoute()
    {
        gameObject.transform.position = new Vector3(Mathf.SmoothStep(farPos.x, startPos.x, travelTime), Mathf.SmoothStep(farPos.y, startPos.y, travelTime), Mathf.SmoothStep(farPos.z, startPos.z, travelTime));
    }

}
