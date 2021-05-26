using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBetweenTwoValues : MonoBehaviour
{
    public Vector2 MinMax;
    public float speed;

    public float timerX = 0f;
    public float timerY = 0f;

    public bool alwaysActive;
    public bool position = true;
    public bool rotation = true;

    private bool isWarp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(alwaysActive == true)
        {
            if (position)
            {
                gameObject.transform.localPosition = new Vector3(Mathf.Lerp(MinMax.x, MinMax.y, (Mathf.Sin(timerX) / 2) + 0.5f), Mathf.Lerp(MinMax.x / 2f, MinMax.y / 2f, (Mathf.Sin(timerY + 1) / 2) + 0.5f), 0);
            }
            if (rotation)
            {
                gameObject.transform.localRotation = new Quaternion(0, 0, Mathf.Lerp(-0.15f, 0.15f, (Mathf.Sin(timerX) / 2) + 0.5f), gameObject.transform.rotation.w);
            }

            timerX += speed * Time.deltaTime;
            timerY = timerX;
        }

        if (Input.GetKeyDown("f") && alwaysActive == false)
        {
            isWarp = !isWarp;
        }

        if(isWarp == true)
        {
            warpMovement();
        }
        else { timerX = 0f; }
    }

    public void warpMovement()
    {
        if (position)
        {
            gameObject.transform.localPosition = new Vector3(Mathf.Lerp(MinMax.x, MinMax.y, (Mathf.Sin(timerX) / 2) + 0.5f), Mathf.Lerp(MinMax.x / 2f, MinMax.y / 2f, (Mathf.Sin(timerY + 1) / 2) + 0.5f), 0);
        }
        if (rotation)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, Mathf.Lerp(-0.15f, 0.15f, (Mathf.Sin(timerX) / 2) + 0.5f), gameObject.transform.rotation.w);
        }
        timerX += speed * Time.deltaTime;
        timerY = timerX;
    }

}
