using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaBehavior : MonoBehaviour
{
    public Vector3 rotationSpeed;
    public float HueShiftSpeed = 1f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 10f)
        {
            timer += (HueShiftSpeed * 0.1f) * Time.deltaTime;
        }
        else { timer = 0f; }

        gameObject.GetComponent<Renderer>().material.SetFloat("HueShift", Mathf.Sin(timer));
        gameObject.transform.Rotate(rotationSpeed.x * Time.deltaTime, rotationSpeed.y * Time.deltaTime, rotationSpeed.z * Time.deltaTime);
    }
}
