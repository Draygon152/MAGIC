// Written by Kevin Chao

using UnityEngine;

public class Nature : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Nature];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Nature;
    }


    public override Color GetElementColor()
    {
        return elementColorDict[ElementTypes.Elements.Nature];
    }
}