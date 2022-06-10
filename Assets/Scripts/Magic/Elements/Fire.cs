// Written by Kevin Chao

using UnityEngine;

public class Fire : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Fire];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Fire;
    }


    public override Color GetElementColor()
    {
        return elementColorDict[ElementTypes.Elements.Fire];
    }
}