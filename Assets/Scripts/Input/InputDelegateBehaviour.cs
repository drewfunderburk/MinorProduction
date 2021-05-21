using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegateBehaviour : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerMovementBehaviour _playerMovement;

    [SerializeField]
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
        _playerControls.Ship.Fire.performed += context => _projectileSpawnerBehaviour.Fire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerMovement.Move(_playerControls.Ship.Movement.ReadValue<Vector2>());
    }
}
