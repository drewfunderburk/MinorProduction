using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMenuBehaviour : MonoBehaviour
{
    [Tooltip("Maximum x movement")]
    [SerializeField] private float _xPositionRange = 1f;

    [Tooltip("Maximum y movement")]
    [SerializeField] private float _yPositionRange = 1f;

    // Original position of the camera
    private Vector3 _originalPosition;
    private Camera _cam;

    private void Start()
    {
        _originalPosition = transform.position;
        _cam = Camera.main;
    }

    private void Update()
    {
        // Cache mouse position
        Vector2 mousePos = Input.mousePosition;

        // Calculate mouse position as a percentage of width and height
        float percentageOfHeight = mousePos.y / _cam.scaledPixelHeight;
        float percentageOfWidth = mousePos.x / _cam.scaledPixelWidth;

        // Map percentage onto maximum allowed movement
        float xMovement = _xPositionRange * (percentageOfWidth - 0.5f);
        float yMovement = _yPositionRange * (percentageOfHeight - 0.5f);
        Vector3 movement = new Vector3(xMovement, yMovement, 0);

        // Move this object to the new desired position
        transform.position = _originalPosition + movement;
    }
}
