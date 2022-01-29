public class Wind : Element
{
    public override string GetElementName()
    {
        return ElementNameDict[ElementTypes.Wind];
    }


    public override ElementTypes GetElementType()
    {
        return ElementTypes.Wind;
    }
}