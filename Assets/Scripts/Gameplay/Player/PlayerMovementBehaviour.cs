using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Tooltip("The Speed at Which The Player Will Move")]
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    [Tooltip("Min X Position that the player will go to")]
    private float _minPosX = 0;

    [SerializeField]
    [Tooltip("Max X Position that the player will go to")]
    private float _maxPosX = 0;

    [SerializeField]
    [Tooltip("Min Z Position that the player will go to")]
    private float _minPosZ = 0;

    [SerializeField]
    [Tooltip("Max Z Position that the player will go to")]
    private float _maxPosZ = 0;

    [Tooltip("The Final Rotation the Player Will Reach During Movement in Degrees")]
    [SerializeField]
    private Vector3 _desiredRotation;

    [Tooltip("The Speed at which the player will reach maximum banking")]
    [SerializeField]
    private float _bankingSpeed = 1;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Prevent the player from moving if the velocity plus its position would put it over and instead add what would be needed
        if(transform.position.x + _velocity.x < _minPosX)
        {
            _velocity.x = _minPosX - transform.position.x;
        }
        else if (transform.position.x + _velocity.x > _maxPosX)
        {
            _velocity.x = _maxPosX - transform.position.x;
        }
        if(transform.position.z + _velocity.y < _minPosZ)
        {
            _velocity.y = _minPosZ - transform.position.z;
        }
        else if(transform.position.z + _velocity.y > _maxPosZ)
        {
            _velocity.y = _maxPosZ - transform.position.z;
        }

        //swap x and y because player does not move on y axis
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
        transform.rotation = new Quaternion(0, 0, Mathf.Lerp(transform.rotation.z, rotate * -direction.x, Time.deltaTime * _bankingSpeed), 1);
     }
}
