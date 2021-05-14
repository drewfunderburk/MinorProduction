using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    // The enemy's target
    [SerializeField] private Transform _target;
    public virtual Transform Target { get => _target; set => _target = value; }

    // How much HP the enemy has. May only be set by derived classes
    [SerializeField] private float _health = 1;
    public float Health { get => _health; protected set => _health = value; }

    // How much Damage the enemy does. May only be set by derived classes
    [SerializeField] private float _damage = 1;
    public float Damage { get => _damage; protected set => _damage = value; }

    // What layers this enemy should collide with
    [SerializeField] private LayerMask _collideWith = ~0;
    protected LayerMask CollideWith { get => _collideWith; set => _collideWith = value; }

    /// <summary>
    /// Cause this enemy to take damage
    /// </summary>
    /// <param name="damage">How much damage to take</param>
    public abstract void TakeDamage(float damage);
}
