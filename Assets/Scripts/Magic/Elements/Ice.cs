// Written by Kevin Chao

public class Ice : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Elements.Ice];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Ice;
    }
}