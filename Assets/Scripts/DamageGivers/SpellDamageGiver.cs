// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

public class SpellDamageGiver : DamageGiver
{
    [SerializeField] private BaseSpell currentSpell;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 3)
        {
            HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

            if (target != null)
                DamageTarget(target, currentSpell.GetSpell().damage);
        }
    }
}