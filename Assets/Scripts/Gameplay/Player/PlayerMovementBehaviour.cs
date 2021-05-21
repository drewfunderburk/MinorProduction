using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : CombatActor
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _velocity;
    [SerializeField]
    private ProjectileSpawnerBehaviour _projectileSpawnerBehaviour;

    [Tooltip("The Speed at Which The Player Will Bank")]
    [SerializeField]
    private float _bankingSpeed = 1;

    [Tooltip("The Speed at Which The Player Will Move")]
    [SerializeField]
    private float _moveSpeed = 1;

    [Tooltip("The Final Rotation the Player Will Reach During Movement in Degrees")]
    [SerializeField]
    private Vector3 _desiredRotation;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    public void Move(Vector3 direction)
    {
        _velocity = direction * _moveSpeed * Time.deltaTime;

        //variable to hold how much this player will be rotating in this frame
        float rotateToThisPoint = 0;

        //depending on the input the player has provided, rotate in different directions
        switch(direction.x)
        {
            //If the D key is pressed
            case 1:
                //Rotate the player clockwise
                rotateToThisPoint -= _bankingSpeed;
                break;
                //If the A key is pressed
            case -1:
                //Rotate the player CounterClockwise
                rotateToThisPoint += _bankingSpeed;
                break;
                //If no key is pressed we should resort back to the default orientation
            case 0:
                rotateToThisPoint = 0;
                break;
        }
        //Clamp to make sure the player doesnt rotate too far
        rotateToThisPoint = Mathf.Clamp(rotateToThisPoint, -_desiredRotation.z, _desiredRotation.z);
        //Perform the actual rotation scaled by time
        transform.rotation = new Quaternion(0, 0, Mathf.Lerp(transform.rotation.z, rotateToThisPoint, _bankingSpeed), 1);
     }
    public void Shoot()
    {
        _projectileSpawnerBehaviour.Fire();
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
