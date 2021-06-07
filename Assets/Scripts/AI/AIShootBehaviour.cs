using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(ProjectileSpawnerBehaviour))]
public class AIShootBehaviour : MonoBehaviour
{
    [Tooltip("What to shoot at. Leave null to fire straight ahead.")]
    [SerializeField] private Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    private ProjectileSpawnerBehaviour _projectileSpawner;

    private void Start()
    {
        _projectileSpawner = GetComponent<ProjectileSpawnerBehaviour>();
    }
    private void Update()
    {
        _projectileSpawner.Fire();
    }
}