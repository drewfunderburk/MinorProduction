using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputEventsBehaviour : MonoBehaviour
{
    private PlayerMovementBehaviour _movement;
    [SerializeField]
    private ProjectileSpawnerBehaviour _gun;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<PlayerMovementBehaviour>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movement.Move(context.ReadValue<Vector2>());
    }

    void OnFire(InputAction.CallbackContext context)
    {
        _gun.Fire();
    }
}
