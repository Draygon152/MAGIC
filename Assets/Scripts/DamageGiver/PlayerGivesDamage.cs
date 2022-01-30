using UnityEngine;

public class PlayerGivesDamage : DamageGiverManager
{
    [SerializeField] private int damageDealt;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Debug.Log("Collided Object!");
            ObjectHealthManager obj = collision.gameObject.GetComponentInParent<ObjectHealthManager>();
            if (obj != null)
                DamageObject(obj, damageDealt);            
        }

        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided Enemy!");
            EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
            if (enemy != null)
                DamageEnemy(enemy, damageDealt);
        }
    }
}