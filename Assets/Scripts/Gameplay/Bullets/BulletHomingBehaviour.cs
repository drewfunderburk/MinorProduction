using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletHomingBehaviour : BulletBehaviour
{
    [SerializeField] private Transform _target = null;
    public Transform Target { get { return _target; } set { _target = value; } }

    [Tooltip("How much force to use when steering towards the target")]
    [SerializeField] private float _steeringForce = 1000f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        // Give initial velocity
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * Speed;

        // Despawn after delay
        Destroy(gameObject, DespawnTime);
    }

    private void Update()
    {
        // No homing if no target
        if (Target == null)
            return;

        // Get direction to player
        Vector3 direction = (Target.position - transform.position).normalized;

        // Apply steering force
        _rigidbody.AddForce(direction * _steeringForce * Time.deltaTime);

        // Clamp velocity to max speed
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, Speed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        CombatActor actor = collider.gameObject.GetComponent<CombatActor>();
        if (actor)
        {
            // Damage the actor
            actor.TakeDamage(Damage);

            // Destroy this object on collision
            Destroy(gameObject);
        }
    }
}
