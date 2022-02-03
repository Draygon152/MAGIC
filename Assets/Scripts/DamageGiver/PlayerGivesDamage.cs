// Written by Liz

using UnityEngine;

public class PlayerGivesDamage : DamageGiverManager
{
    [SerializeField] private int damageDealt;



    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        Debug.Log($"Player collided with {collisionTag}!");

        if (collisionTag == "Object")
        {
            ObjectHealthManager obj = collision.gameObject.GetComponentInParent<ObjectHealthManager>();
            if (obj != null)
                DamageTarget(obj, damageDealt);            
        }

        else if (collisionTag == "Enemy")
        {
            EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
            if (enemy != null)
                DamageTarget(enemy, damageDealt);
        }
    }
}