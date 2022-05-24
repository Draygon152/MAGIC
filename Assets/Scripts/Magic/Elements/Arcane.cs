// Written by Kevin Chao

using UnityEngine;

public class Arcane : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Arcane];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Arcane;
    }


    public override Color GetElementColor()
    {
        return elementColorDict[ElementTypes.Elements.Arcane];
    }
}