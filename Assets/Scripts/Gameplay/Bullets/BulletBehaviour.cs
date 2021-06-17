using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehaviour : MonoBehaviour
{
    [Tooltip("How fast this bullet can move")]
    [SerializeField] private float _speed;

    [Tooltip("How long this bullet will stay in the scene before destroying itself")]
    [SerializeField] private float _despawnTime;

    public float Speed { get => _speed; set => _speed = value; }
    public float DespawnTime { get => _despawnTime; set => _despawnTime = value; }
    public float Damage { get; set; }
}
