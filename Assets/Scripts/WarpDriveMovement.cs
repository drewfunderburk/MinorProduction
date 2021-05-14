using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDriveMovement : MonoBehaviour
{
    public Vector2 MinMax;
    public float speed;

    public float timerX = 0f;
    public float timerY = 0f;

    public bool isWarp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("f"))
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
        gameObject.transform.position = new Vector3(Mathf.Lerp(MinMax.x, MinMax.y, (Mathf.Sin(timerX)/2) + 0.5f), Mathf.Lerp(MinMax.x/2f, MinMax.y/2f, (Mathf.Sin(timerY + 1) / 2) + 0.5f), 0);
        gameObject.transform.rotation = new Quaternion(0, 0, Mathf.Lerp(-0.15f, 0.15f, (Mathf.Sin(timerX) / 2) + 0.5f), gameObject.transform.rotation.w);
        timerX += speed * Time.deltaTime;
        timerY = timerX;
    }

}
