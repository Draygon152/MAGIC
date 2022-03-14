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
        int collisionObjectLayer = 1 << collision.gameObject.layer; //layers are bit indices in a bitstring, if a layer x is allowed
                                                                    //by a mask then there is a 1 in the xth index of the bit string
                                                                    //That can be found by shift 1 (a 1 in the 0th index) left by x
        int enemyOrObjectMask = enemyLayer.value | objectLayer.value; //A mask that includes both enemy and object layers

        if (currentSpell.IsPlayer())
        {
            // Caster is player
            // If collided with objects or enemies:
            Debug.Log($"COLLIDED OBJECT LAYER: {collisionObjectLayer}");
            Debug.Log($"Enemy or object LAYER: {enemyOrObjectMask}");
            Debug.Log($"Logical AND LAYER: {collisionObjectLayer & enemyOrObjectMask}");

            //check if gameObject is in the enmey or object layers
            if ((collisionObjectLayer & enemyOrObjectMask) != 0)
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
            if ((collisionObjectLayer & playerLayer.value) != 0)
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