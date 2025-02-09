﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthBehaviour : CombatActor
{
    [Tooltip("The Max Health That The Player Can Have")]
    [SerializeField]
    private float _maxHealth = 1;
    public float MaxHealth { get => _maxHealth; }

    private Rigidbody _rigidbody;


    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Removes damage from Health, if Health <= 0, destroy this game object
    /// </summary>
    /// <param name="damage">The amount of health to take from the players health</param>
    public override void TakeDamage(float damage)
    {
        Health -= damage;

        OnTakeDamage.Invoke();

        if (Health <= 0)
        {
            OnDeath.Invoke();
            Die();
        }
    }

    /// <summary>
    /// Default things to do on death
    /// </summary>
    protected virtual void Die()
    {
        // Get colliders and gun
        Collider[] colliders = GetComponents<Collider>();
        PlayerShootBehaviour shoot = GetComponent<PlayerShootBehaviour>();

        // Disable this script
        enabled = false;

        // Disable colliders
        if (colliders.Length > 0)
            foreach (Collider collider in colliders)
                collider.enabled = false;

        // Disable the gun
        if (shoot) shoot.enabled = false;
    }

    /// <summary>
    /// Regens health to Health, if health >= maxhealth, cap it.
    /// </summary>
    /// <param name="health">How much health to add to the players health</param>
    public void RegenHealth(float health)
    {
        Health += health;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }
    /// <summary>
    /// Sets the players health to zero, then destroys the player
    /// </summary>
    public void Kill()
    {
        Health = 0;
        OnDeath.Invoke();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Hard sets the players health to be a specific float value, if the health >= maxhealth, cap it
    /// </summary>
    /// <param name="value"></param>
    public void SetHealth(float value)
    {
        Health = value;
        if (Health > MaxHealth)
            Health = MaxHealth;
        else if (Health <= 0)
            Kill();
    }
    private void OnCollisionEnter(Collision collision)
    {
        CombatActor actor = collision.gameObject.GetComponent<CombatActor>();

        if (actor)
            actor.TakeDamage(Damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        CombatActor actor = other.gameObject.GetComponent<CombatActor>();

        if (actor)
            actor.TakeDamage(Damage);
    }
}
