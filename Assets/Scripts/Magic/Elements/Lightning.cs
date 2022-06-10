// Written by Kevin Chao

using UnityEngine;

public class Lightning : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Lightning];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Lightning;
    }


    public override Color GetElementColor()
    {
        return elementColorDict[ElementTypes.Elements.Lightning];
    }
}