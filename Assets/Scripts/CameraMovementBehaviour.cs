using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehaviour : MonoBehaviour
{
    [Tooltip("Transform to target for lerping")]
    [SerializeField] private Transform _targetTransform = null;

    [Tooltip("How long in seconds the transition should last")]
    [SerializeField] private float _transitionDuration = 1f;

    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    private bool _canTransition = true;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.eulerAngles;
    }

    /// <summary>
    /// Starts coroutine to transition camera into warp state
    /// </summary>
    public void TransitionToWarp()
    {
        if (!_canTransition)
            return;

        StartCoroutine(TransitionCoroutine(_initialPosition, _initialRotation, _targetTransform.position, _targetTransform.eulerAngles));
    }

    /// <summary>
    /// Starts coroutine to transition camera out of warp state
    /// </summary>
    public void TransitionFromWarp()
    {
        if (!_canTransition)
            return;

        StartCoroutine(TransitionCoroutine(_targetTransform.position, _targetTransform.eulerAngles, _initialPosition, _initialRotation));
    }

    /// <summary>
    /// Transitions from one transform to another
    /// </summary>
    private IEnumerator TransitionCoroutine(Vector3 fromPosition, Vector3 fromRotation, Vector3 toPosition, Vector3 toRotation)
    {
        // Disallow multiple transitions
        _canTransition = false;

        // Init timer
        float timer = 0;
        
        // Give a timeout condition to avoid infinite loops
        while (timer <= _transitionDuration)
        {
            timer += Time.deltaTime;

            // Get lerp factor so that lerp lasts the right amount of time
            float lerpFactor = timer / _transitionDuration;

            // Lerp position and rotation
            transform.position = Vector3.Lerp(fromPosition, toPosition, lerpFactor);
            transform.eulerAngles = Vector3.Lerp(fromRotation, toRotation, lerpFactor);

            // Wait for next frame
            yield return null;
        }

        // Set the position and rotation to the destination
        transform.position = toPosition;
        transform.eulerAngles = toRotation;

        // Allow new transitions
        _canTransition = true;
    }
}
