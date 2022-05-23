// Written by Kevin Chao

using UnityEngine;

public class Wind : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Wind];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Wind;
    }


    public override Color GetElementColor()
    {
        return elementColorDict[ElementTypes.Elements.Wind];
    }
}