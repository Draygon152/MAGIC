// Written by Kevin Chao

public class Arcane : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Arcane];
    }


    public override ElementTypes GetElementType()
    {
        return ElementTypes.Arcane;
    }
}