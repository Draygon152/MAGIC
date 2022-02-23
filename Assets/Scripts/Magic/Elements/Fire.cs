// Written by Kevin Chao

public class Fire : Element
{
    public override string GetElementName()
    {
        return elementNameDict[ElementTypes.Elements.Fire];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Fire;
    }
}