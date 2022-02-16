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


    public void hit_Effects(Element.ElementTypes currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
    {
        playerinfo = player;
        entity = locationEntity;
        currentSpell = passedSpell;
        switch (currentElement)
        {
            case Element.ElementTypes.Arcane:
                break;
            case Element.ElementTypes.Wind:
                pushBackEffect();
                break;
            case Element.ElementTypes.Fire:
                sustainedDamageEffect();
                break;
            case Element.ElementTypes.Nature:
                break;
            case Element.ElementTypes.Ice:
                break;
            case Element.ElementTypes.Lightning:
                teleportationEffect();
                break;
            default:
                throw new Exception("Element not found");
        }
    }

    public void time_Effects(Element.ElementTypes currentElement, GameObject player, BaseSpell passedSpell)
    {
        playerinfo = player;
        currentSpell = passedSpell;
        switch (currentElement)
        {
            case Element.ElementTypes.Arcane:
                break;
            case Element.ElementTypes.Wind:
                break;
            case Element.ElementTypes.Fire:
                break;
            case Element.ElementTypes.Nature:
                break;
            case Element.ElementTypes.Ice:
                break;
            case Element.ElementTypes.Lightning:
                teleportationEffect();
                break;
            default:
                throw new Exception("Element not found");
        }
    }


    public void teleportationEffect()
    {
        print("WORKS");
        // Teleports on top of enemy, takes damage as a result, WIP
        playerinfo.transform.position = currentSpell.transform.position;
    }



    public void stunEffect()
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
