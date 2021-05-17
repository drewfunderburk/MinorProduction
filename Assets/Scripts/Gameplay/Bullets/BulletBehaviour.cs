using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehaviour : MonoBehaviour
{
    [Tooltip("How fast this bullet can move")]
    [SerializeField] private float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    [Tooltip("How long this bullet will stay in the scene before destroying itself")]
    [SerializeField] private float _despawnTime;
    public float DespawnTime { get => _despawnTime; set => _despawnTime = value; }

    [Tooltip("What layers this bullet should collide with")]
    [SerializeField] private LayerMask _collideWith = ~0;
    public LayerMask CollideWith { get => _collideWith; set => _collideWith = value; }
}
