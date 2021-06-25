using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAsteroids : MonoBehaviour
{
    public GameObject group01;
    public GameObject group02;
    public GameObject group03;
    public GameObject group04;

    public float random01;
    public float random02;
    public float random03;
    public float random04;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoARandomize()
    {
        random01 = Mathf.Round(Random.Range(0,2));
        random02 = Mathf.Round(Random.Range(0, 2));
        random03 = Mathf.Round(Random.Range(0, 2));
        random04 = Mathf.Round(Random.Range(0, 2));

        UpdateGroups();
    }

    public void UpdateGroups()
    {
        if(random01 >= 1)
        {
            group01.SetActive(true);
        }
        else { group01.SetActive(false); }
        
        if (random02 >= 1)
        {
            group02.SetActive(true);
        }
        else { group02.SetActive(false); }
        
        if (random03 >= 1)
        {
            group03.SetActive(true);
        }
        else { group03.SetActive(false); }
        
        if (random04 >= 1)
        {
            group04.SetActive(true);
        }
        else { group04.SetActive(false); }
    }

}
