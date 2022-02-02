// Written by Kevin Chao

public class Ice : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Ice];
    }


    public override ElementTypes GetElementType()
    {
        return ElementTypes.Ice;
    }
}