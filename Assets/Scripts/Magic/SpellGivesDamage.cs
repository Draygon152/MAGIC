using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGivesDamage : DamageGiverManager
{
    [SerializeField] private BaseSpell currentSpell;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Debug.Log("Collided Object!");
            ObjectHealthManager obj = collision.gameObject.GetComponentInParent<ObjectHealthManager>();
            DamageObject(obj);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Blasted Enemy!");
            EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
            DamageEnemy(enemy, currentSpell.SpellToCast.damage);
        }
    }
}