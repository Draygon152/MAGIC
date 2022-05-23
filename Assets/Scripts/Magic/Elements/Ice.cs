// Written by Kevin Chao

using UnityEngine;

public class Ice : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Ice];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Ice;
    }


    public override Color GetElementColor()
    {
        return elementColorDict[ElementTypes.Elements.Ice];
    }
}