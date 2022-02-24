// Written by Liz
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : ScriptableObject
{
    private Element ElementalAffinity;
    private InputDevice pairedDevice;



    public void SetElement(Element elem)
    {
        ElementalAffinity = elem;
    }


    public Element GetElement()
    {
        return ElementalAffinity;
    }


    public void SetInputDevice(InputDevice device)
    {
        pairedDevice = device;
    }


    public InputDevice GetInputDevice()
    {
        return pairedDevice;
    }
}