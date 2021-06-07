using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletStraightBehaviour : BulletBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        // Give this object its initial velocity
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * Speed;

        // Destroy this object after it's time has expired
        Destroy(this.gameObject, DespawnTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Destroy this object on collision
        Destroy(this.gameObject);
    }
}
