//Worked on by Angel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGivesDamage : DamageGiverManager
{
    [SerializeField] private BaseSpell currentSpell; //the spell that is currently being used

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Object") //currently in production
        {
            Debug.Log("Collided Object!");
            ObjectHealthManager obj = collision.gameObject.GetComponentInParent<ObjectHealthManager>();
            DamageObject(obj, currentSpell.SpellToCast.damage);
            Destroy(gameObject); //destroy the spell when it collides
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Blasted Enemy!");
            EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
<<<<<<< Updated upstream
            DamageEnemy(enemy, currentSpell.SpellToCast.damage);
            Destroy(gameObject); //destroy the spell when it collides
=======
            DamageEnemy(enemy, currentSpell.SpellToCast.damage); //damage the enemy that the spell collided with, with the spell's set damage
        }
    }

    public void sustainedDamage(GameObject entity, BaseSpell spellHit) //WIP but would allow the spell to keep doing damage even after impact
    {
        if (entity.tag == "Enemy")
        {
            Debug.Log("Hurt Enemy!");
            EnemyHealthManager enemy = entity.GetComponent<EnemyHealthManager>();
            DamageEnemy(enemy, spellHit.SpellToCast.damage);
>>>>>>> Stashed changes
        }
    }
}