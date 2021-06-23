using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public Vector3 move;

    public float disableAfterSeconds;

    public bool willDisable;


    // Update is called once per frame
    void Update()
    {
        if (willDisable)
        {
            if (disableAfterSeconds > 0)
            {
                disableAfterSeconds -= Time.deltaTime;
            }
            else { gameObject.SetActive(false); }
        }

        gameObject.transform.Translate(new Vector3(move.x, move.y, move.z));
    }


}
