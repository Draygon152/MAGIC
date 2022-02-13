// Written by Kevin Chao

public class Fire : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Elements.Fire];
    }


    public override ElementTypes.Elements GetElementType()
    {
        return ElementTypes.Elements.Fire;
    }
}