// Worked on by Angel
// Modified by Kevin Chao

using UnityEngine;

public class SpellGivesDamage : DamageGiverManager
{
    [SerializeField] private BaseSpell currentSpell;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Debug.Log("Collided Object!");
            ObjectHealthManager obj = collision.gameObject.GetComponentInParent<ObjectHealthManager>();
            if (obj != null)
                DamageTarget(obj, currentSpell.spellToCast.damage);

            Destroy(gameObject); // Destroy the spell when it collides
        }

        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Blasted Enemy!");
            EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
            if (enemy != null)
                DamageTarget(enemy, currentSpell.spellToCast.damage);
        }
    }

    public void sustainedDamage(GameObject entity, BaseSpell spellHit)
    {
        if (entity.tag == "Enemy")
        {
            Debug.Log("Hurting Enemy!");

            EnemyHealthManager enemy = entity.GetComponent<EnemyHealthManager>();
            if (enemy != null)
                DamageTarget(enemy, spellHit.spellToCast.damage);
        }
    }
}