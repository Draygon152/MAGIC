// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

public class SpellDamageGiver : DamageGiver
{
    [SerializeField] private BaseSpell currentSpell;



    private void OnTriggerEnter(Collider collision)
    {
        if (currentSpell.IsPlayer())
        {
            //Caster is player
            // If collided with objects or enemies:
            if (collision.gameObject.layer == 10 || collision.gameObject.layer == 3)
            {
                HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

                if (target != null)
                    DamageTarget(target, currentSpell.GetSpell().damage);
            }
        }

        else
        {
            //Caster is enemy
            // If collided with players:
            if (collision.gameObject.layer == 7)
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