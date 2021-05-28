using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject PlanetGroup;
    public GameObject DifficultPlanet;
    public GameObject EasyPlanet;
    public GameObject BlackouPlane;
    public GameObject PlayerShip;
    public GameObject CameraGroup;

    public Transform ActivePlanet_Offset;
    public float BlackoutPlane_PS_height;
    public float LevelTime = 10f;

    private Transform DifficultPlanet_trans;
    private Transform EasyPlanet_trans;
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
        DifficultPlanet_trans = DifficultPlanet.transform;
        EasyPlanet_trans = EasyPlanet.transform;
        BlackoutPlane_ini_height = BlackouPlane.transform.position.y;
        iniWarpTimer = warpTimer;
        
    }

    // Update is called once per frame
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
    //<---------------------------------------------------<<<PLANET SELECT>>>--------------------------------------------------------------------------->
    public void PlanetSelect()
    {

        warpTimer = iniWarpTimer;
        BlackouPlane.transform.position = new Vector3(0, BlackoutPlane_PS_height, 0);
        DifficultPlanet.transform.position = DifficultPlanet_trans.position;
        EasyPlanet.transform.position = EasyPlanet_trans.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inPlanetSelect = false;
            isWarping = true;
            inLevel = false;
            warpTimerNormalized = 0f;
            warpTimer = 0f;
            CameraGroup.GetComponent<CameraMovement>().ToggleWarp();
        }

        if (PlayerShip.transform.position.z >= 0)
        {
                
            SelectedPlanet = DifficultPlanet;
            PlanetToHide = EasyPlanet;
        }
        else 
        {
            SelectedPlanet = EasyPlanet; PlanetToHide = DifficultPlanet;
        }
    }
    //>--------------------------------------------------->>PLANET SELECT<<<---------------------------------------------------------------------------<

    //<---------------------------------------------------<<<BEGIN WARP>>>--------------------------------------------------------------------------->
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
        }
        if (warpTimerNormalized > 0)
        {
            PlanetGroup.GetComponent<PlanetMovement>().planetEnRoute(warpTimerNormalized);
        }
        BlackouPlane.transform.position = new Vector3(0, BlackoutPlane_ini_height,0);

        SelectedPlanet.transform.localPosition = ActivePlanet_Offset.transform.localPosition;
        SelectedPlanet.transform.localRotation = ActivePlanet_Offset.localRotation;
        SelectedPlanet.transform.localScale = ActivePlanet_Offset.localScale * SelectedPlanet.GetComponent<PlanetBehavior>().generatedSize;
        PlanetToHide.transform.position = new Vector3(0,0,-25000);

    }
    //>--------------------------------------------------->>END BEGINWARP<<<---------------------------------------------------------------------------<

    //<---------------------------------------------------<<<BEGIN ENDWARP>>>--------------------------------------------------------------------------->
    public void EndWarp()
    {
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
        
        }

        PlanetGroup.GetComponent<PlanetMovement>().planetActive(warpTimerNormalized);
        PlanetGroup.GetComponent<PlanetMovement>().levelStatus = true;

    }
    //>--------------------------------------------------->>END ENDWARP<<<---------------------------------------------------------------------------<

}
