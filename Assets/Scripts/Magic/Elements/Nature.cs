// Written by Kevin Chao

public class Nature : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Nature];
    }


    public override ElementTypes GetElementType()
    {
        return ElementTypes.Nature;
    }
}