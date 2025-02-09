﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CombatActor : MonoBehaviour
{
    [Tooltip("This object's health")]
    [SerializeField] private float _health;

    [Tooltip("How much damage this object deals to another")]
    [SerializeField] private float _damage;

    public virtual float Health { get => _health; set => _health = value; }
    public virtual float Damage { get => _damage; set => _damage = value; }

    public UnityEvent OnDeath;
    public UnityEvent OnTakeDamage;

    /// <summary>
    /// Cause this object to take damage
    /// </summary>
    public abstract void TakeDamage(float damage);
}
