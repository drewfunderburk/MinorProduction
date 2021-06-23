using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Tooltip("The Speed at Which The Player Will Move")]
    [SerializeField]
    private float _moveSpeed = 40;

    [Tooltip("Maximum movespeed")]
    [SerializeField] private float _maximumSpeed;

    [Tooltip("The Speed at Which The Player Will Bank")]
    [SerializeField]
    private float _bankingSpeed = 40;

    [Tooltip("The Final Rotation the Player Will Reach During Movement in Degrees")]
    [SerializeField]
    private float _desiredRotation = 45;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = Mathf.Clamp(value, 0, _maximumSpeed); }

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private float _rotateThisMuch;
    private Vector2 _screenDimensions;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Prevent the player from moving if the velocity plus its position would put it over and instead add what would be needed
        if (transform.position.x + _velocity.x < -Screen.safeArea.width / 16)
        {
            _velocity.x = -Screen.safeArea.width / 16 - transform.position.x;
        }
        else if (transform.position.x + _velocity.x > Screen.safeArea.width / 16)
        {
            _velocity.x = Screen.safeArea.width / 16 - transform.position.x;
        }
        if (transform.position.z + _velocity.y < -Screen.safeArea.height / 16)
        {
            _velocity.y = -Screen.safeArea.height / 16 - transform.position.z;
        }
        else if (transform.position.z + _velocity.y > Screen.safeArea.height / 16)
        {
            _velocity.y = Screen.safeArea.height / 16 - transform.position.z;
        }

        //swap x and y because player does not move on y axis
        _rigidbody.MovePosition(_rigidbody.position + new Vector3(_velocity.x, _velocity.z, _velocity.y));
        
        //checks to see if the current rotation plus the desired rotation is farther than the desired final rotation
        if (_rigidbody.rotation.eulerAngles.z + _rotateThisMuch > _desiredRotation && _rigidbody.rotation.eulerAngles.z < 180)
            _rotateThisMuch = _desiredRotation - _rigidbody.rotation.eulerAngles.z;
        else if (_rigidbody.rotation.eulerAngles.z + _rotateThisMuch < (360 - _desiredRotation) && _rigidbody.rotation.eulerAngles.z > 180)
            _rotateThisMuch = -_desiredRotation + 360 - _rigidbody.rotation.eulerAngles.z;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, _rotateThisMuch) * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(deltaRotation * _rigidbody.rotation);
    }

    public void Move(Vector3 direction)
    {
        _rotateThisMuch = 0;
        _velocity = direction * MoveSpeed * Time.deltaTime;

        //depending on the input the player has provided, rotate in different directions
        switch (direction.x)
        {
            //If the D key is pressed
            case 1:
                //Rotate the player clockwise
                _rotateThisMuch -= _bankingSpeed;
                break;
                //If the A key is pressed
            case -1:
                //Rotate the player counter Clockwise
                _rotateThisMuch += _bankingSpeed;
                break;
                //If no key is pressed we should resort back to the default orientation
            case 0:
                //checks to see what hemisphere the player is in to apply a positive or negative rotation
                if (_rigidbody.rotation.eulerAngles.z > 0 && _rigidbody.rotation.eulerAngles.z < 180)
                    _rotateThisMuch = -Mathf.Lerp(_rigidbody.rotation.eulerAngles.z, 360, Time.deltaTime);
                else if (360 - _rigidbody.rotation.eulerAngles.z > 0 && 360 - _rigidbody.rotation.eulerAngles.z < 180)
                    _rotateThisMuch = Mathf.Lerp(360 - _rigidbody.rotation.eulerAngles.z, 0, Time.deltaTime);
                break;
        }
     }

    private void Awake()
    {
        _screenDimensions.x = Screen.safeArea.width;
        _screenDimensions.y = Screen.safeArea.height;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(), new Vector3(_screenDimensions.x / 8, 0, _screenDimensions.y / 8));
    }
}
