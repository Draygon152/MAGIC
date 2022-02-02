//Worked on by Angel

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private GameObject playerinfo;
    private GameObject entity;
    [SerializeField] private SpellGivesDamage ouch;
    private BaseSpell currentSpell;

    public void Base_Effects(Element.ElementTypes currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
    {
        playerinfo = player;
        entity = locationEntity;
        currentSpell = passedSpell;
        switch (currentElement)
        {
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
                break;
            default:
                throw new Exception("Element not found");
        }
    }

    void lightningEffect()
    {
        playerinfo.transform.position = entity.transform.position; //Teleports ontop of enemy, takes damage as a result
    }

    void arcaneEffect()
    {
        print("TESTING1");
    }

    void fireEffect()
    {
        print("tick");
    }

    void windEffect()
    {
        Vector3 direction = entity.transform.position - playerinfo.transform.position;
        entity.GetComponent<Rigidbody>().AddForce(direction * 100);
    }

    void iceEffect()
    {
        print("TESTING1");
    }
}
