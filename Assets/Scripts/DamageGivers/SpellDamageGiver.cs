// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

public class SpellDamageGiver : DamageGiver
{
    [SerializeField] private BaseSpell currentSpell;


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"{currentSpell.name} applying damage to {collision.name}");

        // If collided with objects or enemies:
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 3)
        {
            HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

            if (target != null)
                DamageTarget(target, currentSpell.GetSpell().damage);
        }

        // If collided with players:
        if(collision.gameObject.layer == 7)
        {
            HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

            if (target != null)
                DamageTarget(target, currentSpell.GetSpell().damage);
        }
    }
}