// Written by Kevin Chao

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
}