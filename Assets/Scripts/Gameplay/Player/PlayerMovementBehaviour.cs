using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : CombatActor
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    [Tooltip("The Speed at Which The Player Reaches the Desired Rotation During Movement")]
    [SerializeField]
    private float _bankingSpeed = 1;
    [Tooltip("The Speed at Which The Player Will Move")]
    [SerializeField]
    private float _moveSpeed = 1;
    [Tooltip("The Final Rotation the Player Will Reach During Movement in Degrees")]
    [SerializeField]
    private Vector3 _desiredRotation;
    private Vector3 _currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentRotation = _rigidbody.rotation.eulerAngles;
    }

    public void Move(Vector3 direction)
    {
        _velocity = direction * _moveSpeed * Time.deltaTime;
        Vector3 Rotation = Vector3.Lerp(_currentRotation, _desiredRotation, 1 * _bankingSpeed);

        if (direction.x > 0 && _currentRotation.z < _desiredRotation.z)
            transform.Rotate(-Rotation);
        else if (direction.x < 0 && _currentRotation.z < _desiredRotation.z)
            transform.Rotate(Rotation);


        _currentRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
                                                  //swap x and y because player does not move on y axis
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
