using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class HealPowerUpBehaviour : MonoBehaviour, IPowerUp
{
    [SerializeField] private float _amountToHeal = 0;

    public bool ApplyPowerUp(GameObject obj)
    {
        // Attempt to get a PlayerHealthBehaviour from obj
        PlayerHealthBehaviour health = obj.GetComponentInParent<PlayerHealthBehaviour>();

        // If we were successful, regen health and return true
        if (health)
        {
            health.RegenHealth(_amountToHeal);
            return true;
        }
        else
        {
            // Otherwise log an error and return false
            Debug.LogError("Could not find a PlayerHealthBehaviour");
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
