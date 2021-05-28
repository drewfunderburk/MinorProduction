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

    public Transform CurrentPlanet;
    public float BlackoutPlane_PS_height;

    private Transform DifficultPlanet_trans;
    private Transform EasyPlanet_trans;
    private float BlackoutPlane_ini_height;

    public float warpTimer = 5f;

    public bool planetSelect;
    public bool isWarping;
    private bool LEVEL_START;
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
        if (LEVEL_START == false)
        {
            planetSelect = true;
            DifficultPlanet.GetComponent<PlanetBehavior>().Generate();
            EasyPlanet.GetComponent<PlanetBehavior>().Generate();
            LEVEL_START = true;
        }

        if(planetSelect == true)
        {
            PlanetSelect();
        }

        if(isWarping == true)
        {
            BeginWarp();
        }

    }

    public void PlanetSelect()
    {

        warpTimer = iniWarpTimer;
        BlackouPlane.transform.position = new Vector3(0, BlackoutPlane_PS_height, 0);
        DifficultPlanet.transform.position = DifficultPlanet_trans.position;
        EasyPlanet.transform.position = EasyPlanet_trans.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerShip.transform.position.z >= 0)
            {
                SelectedPlanet = DifficultPlanet;
                PlanetToHide = EasyPlanet;
                isWarping = true;
                warpTimerNormalized = 0f;
                warpTimer = 0f;
                CameraGroup.GetComponent<CameraMovement>().ToggleWarp();
            }
            else
            {
                SelectedPlanet = EasyPlanet; PlanetToHide = DifficultPlanet;
                isWarping = true;
                warpTimerNormalized = 0f;
                warpTimer = 0f;
                CameraGroup.GetComponent<CameraMovement>().ToggleWarp();
            }

        }
    }

    public void BeginWarp()
    {

        if(warpTimerNormalized < 1f)
        {
            warpTimer += 1f * Time.deltaTime;
            warpTimerNormalized = Mathf.InverseLerp(0f , 5f , warpTimer);
        }
        else { EndWarp(); warpTimerNormalized = 0f; warpTimer = 0f; }

        PlanetGroup.GetComponent<PlanetMovement>().planetEnRoute(warpTimerNormalized);
        planetSelect = false;
        BlackouPlane.transform.position = new Vector3(0, BlackoutPlane_ini_height,0);

        SelectedPlanet.transform.position = CurrentPlanet.position;
        SelectedPlanet.transform.rotation = CurrentPlanet.rotation;
        SelectedPlanet.transform.localScale = CurrentPlanet.localScale;
        PlanetToHide.transform.position = new Vector3(0,0,-25000);

    }

    public void EndWarp()
    {
        if (warpTimerNormalized < 1f)
        {
            warpTimer += 1f * Time.deltaTime;
            warpTimerNormalized = Mathf.InverseLerp(0f, 5f, warpTimer);
        }
        else { PlanetSelect(); }

        isWarping = false;
        CameraGroup.GetComponent<CameraMovement>().ToggleWarp();
        PlanetGroup.GetComponent<PlanetMovement>().planetActive(warpTimerNormalized);

    }


}
