// Written by Angel
// Modified by Kevin Chao

using System;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    [SerializeField] private SpellDamageGiver spellDamageGiver;

    private Player player;
    private GameObject target;
    private BaseSpell spell;



    public void BaseEffects(ElementTypes.Elements element, Player player, GameObject target, BaseSpell passedSpell)
    {
        this.player = player;
        this.target = target;
        spell = passedSpell;

        switch (element)
        {
            case ElementTypes.Elements.Arcane:
                break;

            case ElementTypes.Elements.Wind:
                PushBackEffect();
                break;

            case ElementTypes.Elements.Fire:
                break;

            case ElementTypes.Elements.Nature:
                HealEffect();
                break;

            case ElementTypes.Elements.Ice:
                break;

            case ElementTypes.Elements.Lightning:
                TeleportationEffect();
                break;

            default:
                Debug.LogException(new Exception($"Invalid Element '{element}' in BaseEffects.cs"));
                break;
        }
    }


    private void TeleportationEffect()
    {
        Debug.Log("LIGHTNING EFFECT");

        player.transform.position = spell.transform.position;
    }


    private void StunEffect()
    {
        // set speed to 0 for duration (possibly disable damage aswell)
        Debug.Log("ARCANE EFFECT");
    }


    private void SustainedDamageEffect()
    {
        Debug.Log("FIRE EFFECT");

        spellDamageGiver.SustainedDamage(target, spell);
    }


    private void PushBackEffect()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - player.transform.position;
            target.GetComponent<Rigidbody>().AddForce(direction * 250);
        }
    }


    private void SlowEffect()
    {
        // divides speed by percent
        Debug.Log("ICE EFFECT");
    }


    private void HealEffect()
    {
        if (target != null)
            player.GetComponent<PlayerHealthManager>().GainHealth(spell.GetSpell().damage);
    }
}