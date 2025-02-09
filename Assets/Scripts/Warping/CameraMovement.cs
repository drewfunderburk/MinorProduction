﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 iniPos;
    public Vector3 warpPos;
    private Quaternion iniRot;
    private Quaternion warpRot;
    private float lerpTime = 0f;
    private float iniFOV;
    private bool isWarping = false;

    public float enRouteFOV = 30f;
    public float lerpSpeed = 1f;
    public Transform enRouteTransform;
    public GameObject[] warpEffect;
    public ParticleSystem spoolUpParticle;



    void Start()
    {
        iniPos = gameObject.transform.position;
        iniRot = gameObject.transform.rotation;

        if (GetComponentInChildren<Camera>())
        {
            iniFOV = GetComponentInChildren<Camera>().fieldOfView;
        }

    }

    void Update()
    {

        if (isWarping == true)
        {
            Mathf.Clamp01(lerpTime = (lerpTime < 1f ? lerpTime + Time.deltaTime * lerpSpeed : 1f));

            foreach (GameObject particle in warpEffect)
            {
                particle.SetActive(true);

                if (particle.GetComponent<ParticleSystem>() && !particle.GetComponent<ParticleSystem>().isPlaying)
                {
                    particle.SetActive(true);
                    particle.GetComponent<ParticleSystem>().Play();
                }
            }

        }
        else 
        {
            Mathf.Clamp01(lerpTime = (lerpTime > 0f ? lerpTime - Time.deltaTime * lerpSpeed :  0f));

            warpPos = enRouteTransform.transform.position;
            warpRot = enRouteTransform.transform.rotation;

            foreach (GameObject particle in warpEffect)
            {
                particle.SetActive(false);

                if (particle.GetComponent<ParticleSystem>() && particle.GetComponent<ParticleSystem>().isPlaying)
                {
                    particle.GetComponent<ParticleSystem>().Stop();
                    particle.SetActive(false);
                }
            }
        }

        if (GetComponentInChildren<Camera>())
        {
            GetComponentInChildren<Camera>().fieldOfView = Mathf.SmoothStep(iniFOV, enRouteFOV, lerpTime);
        }

        if (lerpTime < 1f || lerpTime > 0)
        {
            transform.position = new Vector3(Mathf.SmoothStep(iniPos.x, warpPos.x, lerpTime), Mathf.SmoothStep(iniPos.y, warpPos.y, lerpTime), Mathf.SmoothStep(iniPos.z, warpPos.z, lerpTime));
            transform.rotation = new Quaternion(Mathf.SmoothStep(iniRot.x, warpRot.x, lerpTime), Mathf.SmoothStep(iniRot.y, warpRot.y, lerpTime), Mathf.SmoothStep(iniRot.z, warpRot.z, lerpTime), Mathf.SmoothStep(iniRot.w, warpRot.w, lerpTime));
        }
    }

    public void ToggleWarp()
    {
        isWarping = !isWarping;
        if (isWarping == true) { SpoolUp(); }
    }

    void SpoolUp()
    {
        if (!spoolUpParticle.isPlaying)
        {
            spoolUpParticle.Play();
        }
    }


}
