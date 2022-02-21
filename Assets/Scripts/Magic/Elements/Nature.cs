// Written by Kevin Chao

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
}