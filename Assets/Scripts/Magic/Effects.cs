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



    public void Base_Effects(ElementTypes.Elements currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
    {
        playerinfo = player;
        entity = locationEntity;
        currentSpell = passedSpell;
        switch (currentElement)
        {
            case ElementTypes.Elements.Arcane:
                arcaneEffect();
                break;
            case ElementTypes.Elements.Wind:
                windEffect();
                break;
            case ElementTypes.Elements.Fire:
                fireEffect();
                break;
            case ElementTypes.Elements.Nature:
                arcaneEffect();
                break;
            case ElementTypes.Elements.Ice:
                iceEffect();
                break;
            case ElementTypes.Elements.Lightning:
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
