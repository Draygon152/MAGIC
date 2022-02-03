// Written by Angel
// Modified by Kevin Chao

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
            if (player != null)
                DamageTarget(player, currentSpell.SpellToCast.damage);

            Destroy(gameObject); // Destroy the spell when it collides
        }
    }
}
