using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGivesPlayerDamage : DamageGiverManager
{
    [SerializeField] private BaseSpell currentSpell;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided Player!");
            PlayerHealthManager player = collision.gameObject.GetComponentInParent<PlayerHealthManager>();
            DamagePlayer(player, currentSpell.SpellToCast.damage);
            Destroy(gameObject); //destroy the spell when it collides
        }
    }
}
