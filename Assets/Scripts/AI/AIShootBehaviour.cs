using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIShootBehaviour : MonoBehaviour
{
    [Tooltip("What to shoot at. Leave null to fire straight ahead.")]
    [SerializeField] private Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    [SerializeField] private ProjectileSpawnerBehaviour _spawner = new ProjectileSpawnerBehaviour();

    private void Update()
    {
        _spawner.Fire();
    }
}