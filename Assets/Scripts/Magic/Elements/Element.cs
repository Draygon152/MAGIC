// Written by Kevin Chao

using UnityEngine;
using System.Collections.Generic;

public abstract class Element : MonoBehaviour
{
    protected Dictionary<ElementTypes.Elements, string> elementNameDict = new Dictionary<ElementTypes.Elements, string>()
    {
        { ElementTypes.Elements.Arcane, "ARCANE" },
        { ElementTypes.Elements.Fire, "FIRE" },
        { ElementTypes.Elements.Ice, "ICE" },
        { ElementTypes.Elements.Nature, "NATURE" },
        { ElementTypes.Elements.Lightning, "LIGHTNING" },
        { ElementTypes.Elements.Wind, "WIND" }
    };

    protected Dictionary<ElementTypes.Elements, Color> elementColorDict = new Dictionary<ElementTypes.Elements, Color>()
    {
        { ElementTypes.Elements.Arcane, new Color(228, 10, 241)},
        { ElementTypes.Elements.Fire, new Color(255, 106, 25) },
        { ElementTypes.Elements.Ice, new Color(81, 209, 245) },
        { ElementTypes.Elements.Nature, new Color(27, 220, 8) },
        { ElementTypes.Elements.Lightning, new Color(255, 248, 45) },
        { ElementTypes.Elements.Wind, new Color(226, 226, 226) }
    };



    public abstract string GetElementName();
    public abstract ElementTypes.Elements GetElementType();
    public abstract Color GetElementColor();
}