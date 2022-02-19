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


    public void Base_Effects(ElementTypes.Elements currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
    {
        playerinfo = player;
        entity = locationEntity;
        currentSpell = passedSpell;
        switch (currentElement)
        {
            case ElementTypes.Elements.Arcane:
                break;
            case ElementTypes.Elements.Wind:
                pushBackEffect();
                break;
            case ElementTypes.Elements.Fire:
                break;
            case ElementTypes.Elements.Nature:
                healEffect();
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



    public void teleportationEffect()
    {
        print("WORKS");
        playerinfo.transform.position = currentSpell.transform.position;

    }



    public void stunEffect()
    {
        //set speed to 0 for duration (possibly disable damage aswell)
        Debug.Log("ARCANE EFFECT");
    }


    private void sustainedDamageEffect()
    {
        ouch.sustainedDamage(entity, currentSpell);
        Debug.Log("FIRE EFFECT");
    }



    private void pushBackEffect()
    {
        if(entity == null)
        {
        }
        else
        {
            Vector3 direction = entity.transform.position - playerinfo.transform.position;
            entity.GetComponent<Rigidbody>().AddForce(direction * 250);
        }

    }


    private void slowEffect()
    {
        //divides speed by percent
        Debug.Log("ICE EFFECT");
    }

    private void healEffect()
    {
        if (entity == null)
        {

        }
        else
        {
            playerinfo.GetComponent<PlayerHealthManager>().GainHealth(currentSpell.SpellToCast.damage);
        }
    }
}
