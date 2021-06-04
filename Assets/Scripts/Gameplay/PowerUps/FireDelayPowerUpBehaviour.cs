using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDelayPowerUpBehaviour : MonoBehaviour, IPowerUp
{
    private enum ChangeType
    {
        ADDITIVE,
        MULTIPLICATIVE
    }

    [Tooltip("How to apply the fire delay change. Additive to simply add or subtract the value, Multiplicative to multiply the current fire rate by the value")]
    [SerializeField] private ChangeType _changeType = ChangeType.MULTIPLICATIVE;
    [SerializeField] private float _fireDelayChange = 0.5f;

    public bool ApplyPowerUp(GameObject obj)
    {
        // Ensure obj has a projectile spawner and get a reference to it
        ProjectileSpawnerBehaviour spawner = obj.GetComponent<ProjectileSpawnerBehaviour>();
        if (spawner == null)
            return false;

        switch (_changeType)
        {
            // If the type is additive, add the fire delay change
            case ChangeType.ADDITIVE:
                spawner.FireDelay += _fireDelayChange;
                return true;
            // If the type is percentile, multiply the fire delay change
            case ChangeType.MULTIPLICATIVE:
                spawner.FireDelay *= _fireDelayChange;
                return true;
            // If somehow neither is true, return false
            default:
                return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the power up was applied, destroy this power up
        if (ApplyPowerUp(other.gameObject))
            Destroy(this.gameObject);
    }
}
