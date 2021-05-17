using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : CombatActor
{
    // The enemy's target
    [SerializeField] private Transform _target;
    public virtual Transform Target { get => _target; set => _target = value; }


    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(this.gameObject);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        CombatActor actor = collision.gameObject.GetComponent<CombatActor>();
        if (actor)
            actor.TakeDamage(Damage);
    }
}
