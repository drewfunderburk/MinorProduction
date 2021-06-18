using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PowerUpMovementBehaviour : MonoBehaviour
{
    [Tooltip("How fast this powerup should move")]
    [SerializeField] private float _speed = 5;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + (-Vector3.forward * _speed * Time.deltaTime));

        // Destroy this power up if it goes too far down screen
        if (transform.position.z < -100)
            Destroy(this.gameObject);
    }
}
