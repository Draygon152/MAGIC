using UnityEngine;
using System.Collections.Generic;

public abstract class Element : MonoBehaviour
{
    public enum ElementTypes
    {
        Arcane,
        Fire,
        Ice,
        Nature,
        Lightning,
        Wind
    }


    protected Dictionary<ElementTypes, string> ElementNameDict = new Dictionary<ElementTypes, string>()
    {
        {ElementTypes.Arcane, "ARCANE"},
        {ElementTypes.Fire, "FIRE"},
        {ElementTypes.Ice, "ICE"},
        {ElementTypes.Nature, "NATURE"},
        {ElementTypes.Lightning, "LIGHTNING"},
        {ElementTypes.Wind, "WIND"}
    };


    public abstract string GetElementName();
    public abstract ElementTypes GetElementType();
}