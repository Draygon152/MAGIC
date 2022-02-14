// Written by Kevin Chao

using UnityEngine;
using System.Collections.Generic;

public abstract class Element : MonoBehaviour
{
    protected Dictionary<ElementTypes.Elements, string> ElementNameDict = new Dictionary<ElementTypes.Elements, string>()
    {
        {ElementTypes.Elements.Arcane, "ARCANE"},
        {ElementTypes.Elements.Fire, "FIRE"},
        {ElementTypes.Elements.Ice, "ICE"},
        {ElementTypes.Elements.Nature, "NATURE"},
        {ElementTypes.Elements.Lightning, "LIGHTNING"},
        {ElementTypes.Elements.Wind, "WIND"}
    };



    public abstract string GetElementName();
    public abstract ElementTypes.Elements GetElementType();
}