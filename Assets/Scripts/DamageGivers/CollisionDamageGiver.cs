// Written by Liz
// Modified by Kevin Chao
using System.Collections;
using UnityEngine;

public class CollisionDamageGiver : DamageGiver
{
    // Damage that will be dealt to entities colliding with this object
    [SerializeField] private int damageDealt;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealthManager player = collision.gameObject.GetComponentInParent<PlayerHealthManager>();
        if (player != null)
        {
            DamageTarget(player, damageDealt);
        }
    }
}