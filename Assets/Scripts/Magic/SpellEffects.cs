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

    public void stuneffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<DebuffManager>() != null)
            {
                target.GetComponent<DebuffManager>().damageChange(spell.GetSpell().effectDuration);
                target.GetComponent<DebuffManager>().speedChange(spell.GetSpell().effectDuration, 0f);
            }
        }
        catch
        { 
        }
    }

    public void sustaineddamageeffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<DebuffManager>() != null)
            {
                target.GetComponent<DebuffManager>().sustainedDamage(spell.GetSpell().effectDuration, spell.GetSpell().damage);
            }
        }
        catch
        {
        }
    }

    public void sloweffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<DebuffManager>() != null)
            {
                target.GetComponent<DebuffManager>().speedChange(spell.GetSpell().effectDuration, 0.5f);
            }
        }
        catch
        {
        }
    }
}