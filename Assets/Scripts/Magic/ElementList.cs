//Worked on by ANgel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementList : MonoBehaviour
{
    //These are the base spells for each of the elements
    [SerializeField] private BaseSpell ArcaneSpell;
    [SerializeField] private BaseSpell GustSpell;
    [SerializeField] private BaseSpell FireSpell;
    [SerializeField] private BaseSpell NatureSpell;
    [SerializeField] private BaseSpell IceSpell;
    [SerializeField] private BaseSpell LightningSpell;

    public BaseSpell Return_Spell(Element.ElementTypes CurrentElement) //Current Element is the selected element for the player
    {
<<<<<<< Updated upstream
        Debug.Log(GustSpell);
        switch(CurrentElement)
=======
        switch(CurrentElement) //Will compare each of the case to see which is the same as the current element, then will return the corresponding spell
>>>>>>> Stashed changes
        {
            case Element.ElementTypes.Arcane:
                Debug.Log("ARCANE TEST");
                Debug.Log(ArcaneSpell);
                return ArcaneSpell;
            case Element.ElementTypes.Wind:
                return GustSpell;
            case Element.ElementTypes.Fire:
                return FireSpell;
            case Element.ElementTypes.Nature:
                return NatureSpell;
            case Element.ElementTypes.Ice:
                return IceSpell;
            case Element.ElementTypes.Lightning:
                return LightningSpell;
            default:
                throw new Exception("Element not found");
        }
    }
}
