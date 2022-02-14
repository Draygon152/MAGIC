// Written by Kevin Chao

public class Wind : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Elements.Wind];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Wind;
    }
}