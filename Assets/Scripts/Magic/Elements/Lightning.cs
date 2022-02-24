// Written by Kevin Chao

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
}