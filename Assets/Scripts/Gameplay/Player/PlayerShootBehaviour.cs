using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootBehaviour : MonoBehaviour
{
    private ProjectileSpawnerBehaviour _gun;

    // Start is called before the first frame update
    void Start()
    {
        _gun = GetComponent<ProjectileSpawnerBehaviour>();
    }
    public void Fire(InputActionPhase context)
    {
        //only fires when the input actiopn is being performed
        if(context == InputActionPhase.Performed)
        _gun.Fire();
    }
}
