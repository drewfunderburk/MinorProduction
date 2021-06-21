using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _xScreenOffset = -50f;
    [SerializeField] private float _bottomScreenOffset = -50f;
    [SerializeField] private float _topScreenOffset = 50f;

    private Vector3 _originalPosition;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private void Start()
    {
        // Cache original position
        _originalPosition = transform.position;

        // Cache camera
        Camera cam = Camera.main;

        // Find screenspace coordinates
        float xScreenPos = Screen.width + _xScreenOffset;
        float topScreenPos = 0 + _bottomScreenOffset;
        float bottomScreenPos = Screen.height + _topScreenOffset;

        // Apply to worldspace
        Vector3 initialScreenPosition = cam.WorldToScreenPoint(_originalPosition);
        _startPosition = cam.ScreenToWorldPoint(new Vector3(xScreenPos, topScreenPos, cam.transform.position.y + initialScreenPosition.z));
        _endPosition = cam.ScreenToWorldPoint(new Vector3(xScreenPos, bottomScreenPos, cam.transform.position.y + initialScreenPosition.z));
    }

    private void Update()
    {
        // Get time left in level as number from 0 - 1
        float time = Mathf.Clamp(GameManagerBehaviour.Instance.TimeLeftInLevel / GameManagerBehaviour.Instance.LevelDuration, 0, 1);

        // Lerp position
        transform.position = Vector3.Lerp(_startPosition, _endPosition, time);
    }

}
