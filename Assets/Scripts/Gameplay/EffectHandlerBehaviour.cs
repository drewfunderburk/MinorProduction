using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandlerBehaviour : MonoBehaviour
{
    private ParticleSystem[] _systems;

    private void Awake()
    {
        _systems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in _systems)
        {
            system.Pause();
            system.Clear();
            system.time = 0;
        }
    }

    public void StartSystems()
    {
        transform.parent = null;

        foreach (ParticleSystem system in _systems)
        {
            system.Play();
        }
    }
}
