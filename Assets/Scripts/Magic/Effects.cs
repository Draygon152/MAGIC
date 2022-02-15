// Written by Angel
// Modified by Kevin Chao

using System;
using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour
{
    private GameObject playerinfo;
    private GameObject entity;
    [SerializeField] private SpellGivesDamage ouch;
    private BaseSpell currentSpell;


<<<<<<< Updated upstream

    public void Base_Effects(Element.ElementTypes currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
=======
    public void hit_Effects(ElementTypes.Elements currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
>>>>>>> Stashed changes
    {
        playerinfo = player;
        entity = locationEntity;
        currentSpell = passedSpell;
        switch (currentElement)
        {
<<<<<<< Updated upstream
            case Element.ElementTypes.Arcane:
                arcaneEffect();
                break;
            case Element.ElementTypes.Wind:
                windEffect();
                break;
            case Element.ElementTypes.Fire:
                fireEffect();
                break;
            case Element.ElementTypes.Nature:
                arcaneEffect();
                break;
            case Element.ElementTypes.Ice:
                iceEffect();
                break;
            case Element.ElementTypes.Lightning:
                lightningEffect();
=======
            case ElementTypes.Elements.Arcane:
                break;
            case ElementTypes.Elements.Wind:
                pushBackEffect();
                break;
            case ElementTypes.Elements.Fire:
                sustainedDamageEffect();
                break;
            case ElementTypes.Elements.Nature:
                break;
            case ElementTypes.Elements.Ice:
                break;
            case ElementTypes.Elements.Lightning:
                teleportationEffect();
                break;
            default:
                throw new Exception("Element not found");
        }
    }

    public void time_Effects(ElementTypes.Elements currentElement, GameObject player, BaseSpell passedSpell)
    {
        playerinfo = player;
        currentSpell = passedSpell;
        switch (currentElement)
        {
            case ElementTypes.Elements.Arcane:
                break;
            case ElementTypes.Elements.Wind:
                break;
            case ElementTypes.Elements.Fire:
                break;
            case ElementTypes.Elements.Nature:
                break;
            case ElementTypes.Elements.Ice:
                break;
            case ElementTypes.Elements.Lightning:
                teleportationEffect();
>>>>>>> Stashed changes
                break;
            default:
                throw new Exception("Element not found");
        }
    }


    private void teleportationEffect()
    {
        // Teleports on top of enemy, takes damage as a result, WIP
        playerinfo.transform.position = currentSpell.transform.position; 
    }


    private void stunEffect()
    {
        Debug.Log("ARCANE EFFECT");
    }


    private void sustainedDamageEffect()
    {
        ouch.sustainedDamage(entity, currentSpell);
        Debug.Log("FIRE EFFECT");
    }



    private void pushBackEffect()
    {
        Vector3 direction = entity.transform.position - playerinfo.transform.position;
        entity.GetComponent<Rigidbody>().AddForce(direction * 250);
    }


    private void slowEffect()
    {
        Debug.Log("ICE EFFECT");
    }

    private void healEffect()
    {
        Debug.Log("Hea");
    }
}
