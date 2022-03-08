// Written by Angel
// Modified by Kevin Chao

using System;
using UnityEngine;

// TODO: Merge ElementList and SpellList functionality in revamped spell system
public class ElementList : MonoBehaviour
{
    [SerializeField] private BaseSpell arcaneSpell;
    [SerializeField] private BaseSpell gustSpell;
    [SerializeField] private BaseSpell fireSpell;
    [SerializeField] private BaseSpell natureSpell;
    [SerializeField] private BaseSpell iceSpell;
    [SerializeField] private BaseSpell lightningSpell;

    public static ElementList Instance
    {
        get;
        private set;
    }



    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        else
        {
            Instance = this;
        }
    }


    private void OnDestroy()
    {
        Instance = null;
    }


    // Current Element is the element selected by a player
    public BaseSpell GetSpell(ElementTypes.Elements currentElement) 
    {
        switch(currentElement)
        {
            case ElementTypes.Elements.Arcane:
                return arcaneSpell;

            case ElementTypes.Elements.Wind:
                return gustSpell;

            case ElementTypes.Elements.Fire:
                return fireSpell;

            case ElementTypes.Elements.Nature:
                return natureSpell;

            case ElementTypes.Elements.Ice:
                return iceSpell;

            case ElementTypes.Elements.Lightning:
                return lightningSpell;

            default:
                Exception ex = new Exception($"Element '{currentElement}' not found");
                Debug.LogException(ex);
                throw ex;
        }
    }
}