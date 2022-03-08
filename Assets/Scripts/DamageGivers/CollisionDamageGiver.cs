// Written by Liz
// Modified by Kevin Chao and Angel Rubio

using UnityEngine;

public class CollisionDamageGiver : DamageGiver
{
    // Damage that will be dealt to entities colliding with this object
    [SerializeField] private int damageDealt;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthManager player = collision.gameObject.GetComponentInParent<PlayerHealthManager>();

            if (player != null)
                DamageTarget(player, damageDealt);
        }
    }


    public int currentDamage()
    {
        return damageDealt;
    }


    public void changeDamage(int newdamage)
    {
        damageDealt = newdamage;
    }
}