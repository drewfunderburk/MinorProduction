using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootBehaviour : MonoBehaviour
{
    private ProjectileSpawnerBehaviour _gun;
    private PlayerMovementBehaviour _playerMovement;
    private float _initialMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _gun = GetComponent<ProjectileSpawnerBehaviour>();
        _playerMovement = GetComponent<PlayerMovementBehaviour>();
        _initialMoveSpeed = _playerMovement.MoveSpeed;
    }
    public void Fire(InputActionPhase context)
    {
        //only fires when the input actiopn is being performed
        //seth - also slows player down when shooting
        if (context == InputActionPhase.Performed)
        {
            _gun.Fire();
            _playerMovement.MoveSpeed = _initialMoveSpeed * _playerMovement._SpeedReductionMultiplier;
        }
        else { _playerMovement.MoveSpeed = _initialMoveSpeed; }
    }
}
