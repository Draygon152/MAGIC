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
        { ElementTypes.Elements.Arcane, new Color(0.8941f, 0.0392f, 0.9451f)},
        { ElementTypes.Elements.Fire, new Color(1.0f, 0.4157f, 0.098f) },
        { ElementTypes.Elements.Ice, new Color(0.3176f, 0.8196f, 0.9608f) },
        { ElementTypes.Elements.Nature, new Color(0.1059f, 0.8627f, 0.0314f) },
        { ElementTypes.Elements.Lightning, new Color(1.0f, 0.9725f, 0.1765f) },
        { ElementTypes.Elements.Wind, new Color(0.8863f, 0.8863f, 0.8863f) }
    };



    public abstract string GetElementName();
    public abstract ElementTypes.Elements GetElementType();
    public abstract Color GetElementColor();
}