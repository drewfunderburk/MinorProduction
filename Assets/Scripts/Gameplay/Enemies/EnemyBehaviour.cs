using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : CombatActor
{
    // The enemy's target
    [SerializeField] private Transform _target;
    public virtual Transform Target { get => _target; set => _target = value; }

    [SerializeField] private int _scoreValue = 10;
    public int ScoreValue { get => _scoreValue; }


    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            GameManagerBehaviour.Instance.IncreaseScore(_scoreValue);
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        CombatActor actor = collision.gameObject.GetComponent<CombatActor>();
        if (actor)
            actor.TakeDamage(Damage);
    }
}
