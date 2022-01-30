public class Fire : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Fire];
    }


    public override ElementTypes GetElementType()
    {
        return ElementTypes.Fire;
    }
}