using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpParticlesBehaviour : MonoBehaviour
{
    [SerializeField] private float _particleStartDelay = 1f;

    private ParticleSystem[] _systems;

    private void Start()
    {
        _systems = GetComponentsInChildren<ParticleSystem>();
    }

    public void StartParticles()
    {
        StartCoroutine(StartParticlesCoroutine());
    }

    private IEnumerator StartParticlesCoroutine()
    {
        yield return new WaitForSeconds(_particleStartDelay);

        foreach (ParticleSystem system in _systems)
        {
            system.Play();
        }
    }
}
