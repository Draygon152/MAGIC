// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using System;

public class ElementList : MonoBehaviour
{
    [SerializeField] private BaseSpell ArcaneSpell;
    [SerializeField] private BaseSpell GustSpell;
    [SerializeField] private BaseSpell FireSpell;
    [SerializeField] private BaseSpell NatureSpell;
    [SerializeField] private BaseSpell IceSpell;
    [SerializeField] private BaseSpell LightningSpell;



    // Current Element is the element selected by a player
    public BaseSpell GetSpell(ElementTypes.Elements CurrentElement) 
    {
        switch(CurrentElement)
        {
            case ElementTypes.Elements.Arcane:
                Debug.Log(ArcaneSpell);
                return ArcaneSpell;
            case ElementTypes.Elements.Wind:
                return GustSpell;
            case ElementTypes.Elements.Fire:
                return FireSpell;
            case ElementTypes.Elements.Nature:
                return NatureSpell;
            case ElementTypes.Elements.Ice:
                return IceSpell;
            case ElementTypes.Elements.Lightning:
                return LightningSpell;
            default:
                throw new Exception("Element not found");
        }
    }
}
