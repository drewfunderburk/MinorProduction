﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : CombatActor
{
    // The enemy's target
    [SerializeField] private Transform _target;
    public virtual Transform Target { get => _target; set => _target = value; }

    [Tooltip("Score value for this enemy. Scales with level")]
    [SerializeField] private int _scoreValue = 3;
    public virtual int ScoreValue { get => _scoreValue * GameManagerBehaviour.Instance.Level; }

    public override void TakeDamage(float damage)
    {
        Health -= damage;

        OnTakeDamage.Invoke();

        if (Health <= 0)
        {
            GameManagerBehaviour.Instance.IncreaseScore(_scoreValue);
            OnDeath.Invoke();
            Die();
            Destroy(gameObject, 3);
        }

    }

    /// <summary>
    /// Default things to do on death
    /// </summary>
    protected virtual void Die()
    {
        // Get colliders and gun
        Collider[] colliders = GetComponents<Collider>();
        AIShootBehaviour shoot = GetComponent<AIShootBehaviour>();

        // Disable this script
        enabled = false;

        // Disable colliders
        if (colliders.Length > 0)
            foreach (Collider collider in colliders)
                collider.enabled = false;

        // Disable the gun
        if (shoot) shoot.enabled = false;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        CombatActor actor = collision.gameObject.GetComponent<CombatActor>();
        if (actor)
            actor.TakeDamage(Damage);
    }
}
