// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

public class SpellDamageGiver : DamageGiver
{
    [SerializeField] private BaseSpell currentSpell;



    public void SustainedDamage(GameObject target, BaseSpell spellHit)
    {
        if (target.tag == "Enemy")
        {
            Debug.Log($"Applying {spellHit.name} DOT to {target.name}");

            EnemyHealthManager enemy = target.GetComponent<EnemyHealthManager>();

            if (enemy != null)
                DamageTarget(enemy, spellHit.GetSpell().damage);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"{currentSpell.name} applying damage to {collision.name}");

        if (collision.gameObject.tag == "Object" || collision.gameObject.tag == "Enemy")
        {
            HealthManager target = collision.gameObject.GetComponentInParent<HealthManager>();

            if (target != null)
                DamageTarget(target, currentSpell.GetSpell().damage);
        }
    }
}