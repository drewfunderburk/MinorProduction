﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{

    public Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(speed.x * Time.deltaTime, speed.y * Time.deltaTime, speed.z * Time.deltaTime));
    }
}
