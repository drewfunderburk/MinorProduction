using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBetweenTwoValues : MonoBehaviour
{
    public GameObject WarpManager;

    public Vector2 MinMax;
    public float speed;

    public float timerX = 0f;

    public bool alwaysActive;
    public bool position = true;
    public bool rotation = true;

    private bool isWarp;
    private Vector3 iniPos;
    private Quaternion iniRot;
    private Vector3 lastPos;
    private Quaternion lastRot;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = gameObject.transform.localPosition;
        iniRot = gameObject.transform.localRotation;
        lastPos = gameObject.transform.localPosition;
        lastRot = gameObject.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (WarpManager.GetComponent<WarpManager>().isWarping)
        {
            isWarp = true;
        }
        else { isWarp = false; }

        if(alwaysActive == true)
        {
            if (position)
            {
                gameObject.transform.localPosition = new Vector3(Mathf.Lerp(MinMax.x, MinMax.y, (Mathf.Sin(timerX) / 2) + 0.5f), Mathf.Lerp(MinMax.x / 2f, MinMax.y / 2f, (Mathf.Sin(timerX + 1) / 2) + 0.5f), 0);
            }
            if (rotation)
            {
                gameObject.transform.localRotation = new Quaternion(0, 0, Mathf.Lerp(-0.15f, 0.15f, (Mathf.Sin(timerX) / 2) + 0.5f), gameObject.transform.rotation.w);
            }

            timerX += speed * Time.deltaTime;
        }

        
        if(isWarp == true)
        {
            warpMovement();
        }
        else 
        {
            if(timerX > 1f)
            {
                timerX = 1f;
            }

            if (timerX > 0)
            {
                if (position)
                {
                    gameObject.transform.localPosition = new Vector3(Mathf.SmoothStep(iniPos.x, lastPos.x, timerX), Mathf.SmoothStep(iniPos.y, lastPos.y, timerX), 0);
                }
                if (rotation)
                {
                    gameObject.transform.localRotation = new Quaternion(0, 0, Mathf.SmoothStep(iniRot.z, lastRot.z, timerX), gameObject.transform.localRotation.w);
                }

                timerX -= speed * 2f * Time.deltaTime;
            }
            
            
        }
    }

    public void warpMovement()
    {
        if (position)
        {
            gameObject.transform.localPosition = new Vector3(Mathf.Lerp(MinMax.x, MinMax.y, (Mathf.Sin(timerX) / 2) + 0.5f), Mathf.Lerp(MinMax.x / 2f, MinMax.y / 2f, (Mathf.Sin(timerX + 1) / 2) + 0.5f), 0);
        }
        if (rotation)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, Mathf.Lerp(-0.15f, 0.15f, (Mathf.Sin(timerX) / 2) + 0.5f), gameObject.transform.localRotation.w);
        }

        timerX += speed * Time.deltaTime;

        lastPos = gameObject.transform.localPosition;
        lastRot = gameObject.transform.localRotation;
    }

}
