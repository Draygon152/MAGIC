// Written by Liz
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : ScriptableObject
{
    private Element ElementalAffinity;
    public InputDevice pairedDevice;

    public void SetElement(Element elem)
    {
        ElementalAffinity = elem;
    }


    public Element GetElement()
    {
        return ElementalAffinity;
    }
}