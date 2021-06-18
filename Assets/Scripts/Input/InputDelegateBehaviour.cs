using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegateBehaviour : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerMovementBehaviour _playerMovement;
    private PlayerShootBehaviour _playerShootBehaviour;
    private ProjectileSpawnerBehaviour _projectileSpawnerBehaviour;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovementBehaviour>();
        _playerShootBehaviour = GetComponent<PlayerShootBehaviour>();
        _projectileSpawnerBehaviour = GetComponent<ProjectileSpawnerBehaviour>();
        _playerControls.Ship.Fire.performed += context => _projectileSpawnerBehaviour.Fire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //calls the players movement behaviour's move function using read values as input
        _playerMovement.Move(_playerControls.Ship.Movement.ReadValue<Vector2>());
        //calls the shoot behaviours fire using the action phase of Fire
        _playerShootBehaviour.Fire(_playerControls.Ship.Fire.phase);
    }
}
