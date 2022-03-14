// Written by Angel
// Modified by Kevin Chao

using System;
using UnityEngine;

public class SpellDamageGiver : DamageGiver
{
    [SerializeField] private BaseSpell currentSpell;
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask playerLayer;



    private void OnTriggerEnter(Collider collision)
    {
        if (currentSpell.IsPlayer())
        {
            // Caster is player
            // If collided with objects or enemies:
            Debug.Log($"COLLIDED OBJECT LAYER: {collision.gameObject.layer}");
            Debug.Log($"OBJECT LAYER: {Convert.ToInt32(objectLayer.value)}");
            Debug.Log($"ENEMY LAYER: {Convert.ToInt32(enemyLayer.value)}");

            if (collision.gameObject.layer == objectLayer.value || collision.gameObject.layer == enemyLayer.value)
            {
                HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

                if (target != null)
                    DamageTarget(target, currentSpell.GetSpell().damage);
            }
        }

        else
        {
            // Caster is enemy
            // If collided with players:
            if (collision.gameObject.layer == playerLayer.value)
            {
                HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

                if (target != null)
                    DamageTarget(target, currentSpell.GetSpell().damage);
            }
        }
    }


    public void UseDamage(GameObject subject, int damage)
    {
        HealthManager subjecthealth = subject.GetComponentInParent<HealthManager>();
        DamageTarget(subjecthealth, damage);
    }
}