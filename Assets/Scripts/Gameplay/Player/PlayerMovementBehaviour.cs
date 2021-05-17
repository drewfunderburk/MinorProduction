using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : CombatActor
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    [SerializeField]
    private float _bankingSpeed = 1;
    [SerializeField]
    private float _moveSpeed = 1;
    private float _currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentRotation = _rigidbody.rotation.z;
    }

    public void Move(Vector3 direction)
    {
        _velocity = direction * _moveSpeed * Time.deltaTime;
        _rigidbody.rotation.z += (direction.x / Mathf.Abs(direction.x));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(_velocity.x,_velocity.z,_velocity.y));
    }

    public override void TakeDamage(float damage)
    {
        Health -= 1;
        if (Health <= 0)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        CombatActor actor = collision.gameObject.GetComponent<CombatActor>();

        if (actor)
            actor.TakeDamage(Damage);
    }
}
