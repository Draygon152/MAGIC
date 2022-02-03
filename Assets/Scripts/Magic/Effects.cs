// Written by Angel
// Modified by Kevin Chao

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


    private void lightningEffect()
    {
        // Teleports on top of enemy, takes damage as a result, WIP
        playerinfo.transform.position = entity.transform.position; 
    }


    private void arcaneEffect()
    {
        Debug.Log("ARCANE EFFECT");
    }


    private void fireEffect()
    {
        Debug.Log("FIRE EFFECT");
    }


    private void windEffect()
    {
        Vector3 direction = entity.transform.position - playerinfo.transform.position;
        entity.GetComponent<Rigidbody>().AddForce(direction * 100);
    }


    private void iceEffect()
    {
        Debug.Log("ICE EFFECT");
    }
}
