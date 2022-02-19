// Written by Liz
// Modified by Kevin Chao

using UnityEngine;

// [CreateAssetMenu(fileName = "New PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    private Element ElementalAffinity;

    public void SetElement(Element elem)
    {
        ElementalAffinity = elem;
    }

    public Element GetElement()
    {
        return ElementalAffinity;
    }
}