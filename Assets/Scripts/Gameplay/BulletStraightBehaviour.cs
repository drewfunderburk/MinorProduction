using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletStraightBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    public float Speed { get; set; }

    [SerializeField] private float _despawnTime = 5f;
    public float DespawnTime { get; set; }

    [SerializeField] private LayerMask _collideWith = ~0;

    private Rigidbody _rigidbody;

    private void Start()
    {
        // Give this object its initial velocity
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * _speed;

        // Destroy this object after it's time has expired
        Destroy(this.gameObject, _despawnTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the collided object's layer is in _collideWith, destroy this object
        if (((1 << collision.gameObject.layer) & _collideWith) != 0)
            Destroy(this.gameObject);
    }
}
