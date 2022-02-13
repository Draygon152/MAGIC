// Written by Kevin Chao

public class Nature : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Elements.Nature];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Nature;
    }
}