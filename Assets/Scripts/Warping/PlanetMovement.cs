using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public Vector3 farPos;
    public Vector3 nearPos;
    public float farScale;
    public float nearScale = 2f;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool inLevel = false;
    public bool inWarp = false;
    public GameObject EasyPlanet;
    public GameObject HardPlanet;
    public Transform planetWarpPos;

    public PlanetSelectionScreenBehaviour planetSelectScript;

    public float warpDurationNormal = 0f;
    public float levelDurationNormal = 0f;

    private GameObject SelectedPlanet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (planetSelectScript.easyPlanetSelected)
        {
            SelectedPlanet = EasyPlanet;
        }
        else { SelectedPlanet = HardPlanet; }

        if (inLevel == false && inWarp == false)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObject.transform.position = new Vector3(0f, 0f, 0f);
            SelectedPlanet = null;
        }
        else
        {
            if (inLevel)
            {
                planetActive();
            }
            else
            {
                if (inWarp)
                {
                    planetEnRoute();
                }
            }

            SelectedPlanet.transform.position = planetWarpPos.position;
            SelectedPlanet.transform.rotation = planetWarpPos.rotation;
            SelectedPlanet.transform.localScale = planetWarpPos.localScale;

        }

            warpDurationNormal = Mathf.Clamp01(GameManagerBehaviour.Instance.TimeLeftInWarp / GameManagerBehaviour.Instance.WarpDuration);

            levelDurationNormal = Mathf.Clamp01(GameManagerBehaviour.Instance.TimeLeftInLevel / GameManagerBehaviour.Instance.LevelDuration);

        if(GameManagerBehaviour.Instance.TimeLeftInLevel > 0 && GameManagerBehaviour.Instance.TimeLeftInLevel < GameManagerBehaviour.Instance.LevelDuration)
        {
            inLevel = true;
        }
        else { inLevel = false; }

        if (GameManagerBehaviour.Instance.TimeLeftInWarp > 0 && GameManagerBehaviour.Instance.TimeLeftInWarp < GameManagerBehaviour.Instance.WarpDuration)
        {
            inWarp = true;
        }
        else { inWarp = false; }

    }

    public void planetEnRoute()
    {
        gameObject.transform.position = new Vector3(Mathf.SmoothStep(nearPos.x, farPos.x, warpDurationNormal), Mathf.SmoothStep(nearPos.y, farPos.y, warpDurationNormal), Mathf.SmoothStep(nearPos.z, farPos.z, warpDurationNormal));
        gameObject.transform.localScale = new Vector3(Mathf.SmoothStep(nearScale, farScale, warpDurationNormal), Mathf.SmoothStep(nearScale, farScale, warpDurationNormal), Mathf.SmoothStep(nearScale, farScale, warpDurationNormal));
        
    }

    public void planetActive()
    {
        gameObject.transform.position = new Vector3(Mathf.SmoothStep(endPos.x, startPos.x, levelDurationNormal), Mathf.SmoothStep(endPos.y, startPos.y, levelDurationNormal), Mathf.SmoothStep(endPos.z, startPos.z, levelDurationNormal));
        
    }

}
