using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGivesDamage : DamageGiverManager
{
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
            Debug.Log("Collided Enemy!");
            EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
            DamageEnemy(enemy);
        }
    }
}
