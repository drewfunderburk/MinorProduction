using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBehaviour : MonoBehaviour
{
    [Tooltip("The Max Health That The Player Can Have")]
    [SerializeField]
    private float _maxHealth = 1;
    [Tooltip("The Player's Current Health")]
    [SerializeField]
    private float _health = 1;
    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// Subtracts damage from the players health
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health < 0)
            _health = 0;
    }
    /// <summary>
    /// Adds health to the players health
    /// </summary>
    /// <param name="health"></param>
    public void RegenHealth(float health)
    {
        _health += health;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }
    public void Kill()
    {
        _health = 0;
    }
    public void SetHealth(float value)
    {
        _health = value;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }
}
