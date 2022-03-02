// Written by Angel
// Modified by Kevin Chao

using System;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    [SerializeField] private SpellDamageGiver spellDamageGiver;


    public void TeleportationEffect(Player player, GameObject target, BaseSpell spell)
    {
        player.transform.position = spell.transform.position;
    }

    public void pushbackeffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<Rigidbody>() != null)
            {
                Vector3 direction = target.transform.position - player.transform.position;
                if(direction[0] > 1 || direction[0] < -1)
                {
                    direction[0] *= 5;
                }
                if(direction[2] > 1 || direction[2] < -1)
                {
                    direction[2] *= 5;
                }
                target.GetComponent<Rigidbody>().AddForce(direction * 250);
            }
        }
        catch
        {
        }
    }

    public void healeffect(Player player, GameObject target, BaseSpell spell)
    {
        if (target != null)
            player.GetComponent<PlayerHealthManager>().GainHealth(spell.GetSpell().damage);
    }

    /*
    private void stuneffect()
    {
        // set speed to 0 for duration (possibly disable damage aswell)
        debug.log("arcane effect");
    }


    private void sustaineddamageeffect()
    {
        debug.log("fire effect");

        spelldamagegiver.sustaineddamage(target, spell);
    }

    private void sloweffect()
    {
        // divides speed by percent
        debug.log("ice effect");
    }
    */
}