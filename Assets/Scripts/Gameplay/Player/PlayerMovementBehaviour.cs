using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : CombatActor
{
    [SerializeField]
    private ProjectileSpawnerBehaviour _projectileSpawnerBehaviour;

    [Tooltip("The Speed at Which The Player Will Move")]
    [SerializeField]
    private float _moveSpeed = 1;

    [Tooltip("The Final Rotation the Player Will Reach During Movement in Degrees")]
    [SerializeField]
    private Vector3 _desiredRotation;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {                                         //swap x and y because player does not move on y axis
        _rigidbody.MovePosition(transform.position + new Vector3(_velocity.x, _velocity.z, _velocity.y));
    }

    public void Move(Vector3 direction)
    {
        _velocity = direction * _moveSpeed * Time.deltaTime;

        //variable to hold how much this player will be rotating in this frame
        float rotate = transform.rotation.eulerAngles.z;

        //depending on the input the player has provided, rotate in different directions
        switch(direction.x)
        {
            //If the D key is pressed
            case 1:
                //Rotate the player clockwise
                rotate -= Time.deltaTime;
                break;
                //If the A key is pressed
            case -1:
                //Rotate the player CounterClockwise
                rotate += Time.deltaTime;
                break;
                //If no key is pressed we should resort back to the default orientation
            case 0:
                rotate = 0;
                break;
        }
        //Clamp to make sure the player doesnt rotate too far
        rotate = Mathf.Clamp(rotate, -_desiredRotation.z * (Mathf.PI/180), _desiredRotation.z * (Mathf.PI / 180));
        //Perform the actual rotation scaled by time
        transform.rotation = new Quaternion(0, 0, Mathf.Lerp(transform.rotation.z, rotate * -direction.x, Time.deltaTime), 1);
     }
    public void Shoot()
    {
        _projectileSpawnerBehaviour.Fire();
    }

    /// <summary>
    /// Removes damage from Health, if Health <= 0, destroy this game object
    /// </summary>
    /// <param name="damage">The amount of health to take from the players health</param>
    public override void TakeDamage(float damage)
    {
        Health -= damage;
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
