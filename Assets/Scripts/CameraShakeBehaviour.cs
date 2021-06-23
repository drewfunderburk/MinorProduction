using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeBehaviour : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakeIntensity = 1.0f;
    [SerializeField] private float _shakeSpeedFactor = 0.5f;

    private Vector3 _originalPosition;
    private bool _canShake = true;
    private float _shakeTimer = 0;

    public void Shake()
    {
        if (!_canShake)
            return;

        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Debug.Log("Shake");
        // Disallow shaking while already shaking
        _canShake = false;
        
        // Cache original position
        _originalPosition = transform.position;
        Vector3 position = _originalPosition;

        // Reset shake timer
        _shakeTimer = 0;

        while (_shakeTimer < _shakeDuration)
        {
            // Increment timer
            _shakeTimer += Time.deltaTime;
            float intensity = _shakeIntensity * (1 - (_shakeTimer / _shakeDuration));
            if (Vector3.Distance(transform.position, position) < 0.1f)
                position = _originalPosition + (Random.insideUnitSphere * intensity);

            // Lerp towards new position
            transform.position = Vector3.Lerp(transform.position, position, _shakeSpeedFactor);

            yield return null;
        }

        // Allow shaking again
        _canShake = true;
    }
}
