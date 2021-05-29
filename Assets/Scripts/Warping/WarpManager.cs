using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject PlanetGroup;
    public GameObject DifficultPlanet;
    public GameObject EasyPlanet;
    public GameObject BlackoutPlane;
    public GameObject PlayerShip;
    public GameObject CameraGroup;

    public Transform ActivePlanet_Offset;
    public float LevelTime = 10f;

    private Vector3 DifficultPlanet_pos;
    private Vector3 EasyPlanet_pos;
    private float BlackoutPlane_ini_height;

    public float warpTimer = 5f;

    public bool inPlanetSelect = true;
    public bool isWarping;
    public bool inLevel;

    private bool GENERATE_NEW_PLANETS = true;
    private float iniWarpTimer;
    public float warpTimerNormalized = 0f;

    public GameObject SelectedPlanet = null;
    public GameObject PlanetToHide = null;

    // Start is called before the first frame update
    void Start()
    {
        DifficultPlanet_pos = DifficultPlanet.transform.position;
        EasyPlanet_pos = EasyPlanet.transform.position;
        iniWarpTimer = warpTimer;
        
    }

    //<---------------------------------------------------<<< UPDATE >>>--------------------------------------------------------------------------->
    void Update()
    {
        if (GENERATE_NEW_PLANETS == true)
        {
            DifficultPlanet.GetComponent<PlanetBehavior>().Generate();
            EasyPlanet.GetComponent<PlanetBehavior>().Generate();
            GENERATE_NEW_PLANETS = false;
            inPlanetSelect = true;
        }

        if(inPlanetSelect == true && inLevel == false && isWarping == false)
        {
            PlanetSelect();
            Debug.Log("PlanetSelect");
        }

        if (inPlanetSelect == false && inLevel == false && isWarping == true)
        {
            BeginWarp();
            Debug.Log("BeginWarp");
        }

        if (inPlanetSelect == false && inLevel == true && isWarping == false)
        {
            EndWarp();
            Debug.Log("EndWarp");
        }

        

    }
    //>--------------------------------------------------->>UPDATE<<<---------------------------------------------------------------------------<

    //<---------------------------------------------------<<< PLANET SELECT >>>--------------------------------------------------------------------------->
    public void PlanetSelect()
    {

        warpTimer = iniWarpTimer;
        DifficultPlanet.transform.position = DifficultPlanet_pos;
        EasyPlanet.transform.position = EasyPlanet_pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inPlanetSelect = false;
            isWarping = true;
            inLevel = false;
            warpTimerNormalized = 0f;
            warpTimer = 0f;
            CameraGroup.GetComponent<CameraMovement>().ToggleWarp();
        }

        if (PlayerShip.transform.position.x >= 0)
        {
                
            SelectedPlanet = DifficultPlanet;
            PlanetToHide = EasyPlanet;
            SelectedPlanet.GetComponentInChildren<Renderer>().material.SetFloat("IsSelected", 1f);
            PlanetToHide.GetComponentInChildren<Renderer>().material.SetFloat("IsSelected", 0f);
        }
        else 
        {
            SelectedPlanet = EasyPlanet; 
            PlanetToHide = DifficultPlanet;
            SelectedPlanet.GetComponentInChildren<Renderer>().material.SetFloat("IsSelected", 1f);
            PlanetToHide.GetComponentInChildren<Renderer>().material.SetFloat("IsSelected", 0f);
        }
    }
    //>--------------------------------------------------->>PLANET SELECT<<<---------------------------------------------------------------------------<

    //<---------------------------------------------------<<< BEGIN WARP >>>--------------------------------------------------------------------------->
    public void BeginWarp()
    {
        if (warpTimerNormalized < 1f)
        {
            warpTimer += 1f * Time.deltaTime;
            warpTimerNormalized = Mathf.InverseLerp(0f , 5f , warpTimer);
        }
        else
        {
            inPlanetSelect = false;
            isWarping = false;
            inLevel = true;
            warpTimerNormalized = 0f; 
            warpTimer = 0f;
            CameraGroup.GetComponent<CameraMovement>().ToggleWarp();
            PlanetGroup.GetComponent<PlanetMovement>().levelStatus = true;

        }
        if (warpTimerNormalized > 0)
        {
            PlanetGroup.GetComponent<PlanetMovement>().planetEnRoute(warpTimerNormalized);
        }

        SelectedPlanet.transform.localPosition = ActivePlanet_Offset.transform.localPosition;
        SelectedPlanet.transform.localRotation = ActivePlanet_Offset.localRotation;
        SelectedPlanet.transform.localScale = ActivePlanet_Offset.localScale * SelectedPlanet.GetComponent<PlanetBehavior>().generatedSize;
        PlanetToHide.transform.position = new Vector3(0,0,-25000);

    }
    //>--------------------------------------------------->>>BEGIN WARP<<<---------------------------------------------------------------------------<

    //<---------------------------------------------------<<< END WARP >>>--------------------------------------------------------------------------->
    public void EndWarp()
    {
        //This is the level timer
        if (warpTimerNormalized < 1f)
        {
            warpTimer += 1f * Time.deltaTime;
            warpTimerNormalized = Mathf.InverseLerp(0f, LevelTime, warpTimer);
        }
        else 
        {
            inPlanetSelect = false;
            isWarping = false;
            inLevel = false; 
            GENERATE_NEW_PLANETS = true;
            PlanetGroup.GetComponent<PlanetMovement>().levelStatus = false;
            PlanetGroup.GetComponent<PlanetMovement>().planetActive(0f);
            PlanetGroup.GetComponent<PlanetMovement>().planetEnRoute(0f);
            DifficultPlanet.transform.localPosition = DifficultPlanet_pos;
            EasyPlanet.transform.localPosition = EasyPlanet_pos;
        }

        PlanetGroup.GetComponent<PlanetMovement>().planetActive(warpTimerNormalized);

    }
    //>--------------------------------------------------->>ENDWARP<<<---------------------------------------------------------------------------<

}
