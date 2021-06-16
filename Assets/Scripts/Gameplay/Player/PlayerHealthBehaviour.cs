using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthBehaviour : CombatActor
{
    [Tooltip("The Max Health That The Player Can Have")]
    [SerializeField]
    private float _maxHealth = 1;
    public float MaxHealth { get => _maxHealth; }

    public UnityEvent OnDeath;

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
        if (Health <= 0)
        {
            OnDeath.Invoke();
            gameObject.SetActive(false);
        }
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
        _rigidbody.useGravity = true;
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
}
