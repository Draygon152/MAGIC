// Written by Kevin Chao

public class Lightning : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Lightning];
    }


    public override ElementTypes GetElementType()
    {
        return ElementTypes.Lightning;
    }
}