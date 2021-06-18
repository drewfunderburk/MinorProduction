using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpeedPowerUpBehaviour : MonoBehaviour, IPowerUp
{
    private enum ChangeType
    {
        ADDITIVE,
        MULTIPLICATIVE
    }

    [Tooltip("How to apply the speed change. Additive to simply add or subtract the value, Multiplicative to multiply the current speed by the value")]
    [SerializeField] private ChangeType _changeType = ChangeType.MULTIPLICATIVE;
    [SerializeField] private float _speedChange = 1.2f;


    public bool ApplyPowerUp(GameObject obj)
    {
        // Ensure obj has a player movement behaviour and get a reference to it
        PlayerMovementBehaviour movement = obj.GetComponentInParent<PlayerMovementBehaviour>();
        if (movement == null)
            return false;

        switch (_changeType)
        {
            // If the type is additive, add the speed change
            case ChangeType.ADDITIVE:
                movement.MoveSpeed += _speedChange;
                return true;
            // If the type is percentile, multiply the speed change
            case ChangeType.MULTIPLICATIVE:
                movement.MoveSpeed *= _speedChange;
                return true;
            // If somehow neither is true, return false
            default:
                return false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the power up was applied, destroy this power up
        if (ApplyPowerUp(collision.gameObject))
            Destroy(gameObject);
    }
}
