// Written by Angel
// Modified by Kevin Chao

using System.Collections.Generic;
using UnityEngine;

// TODO: Merge ElementList and SpellList functionality in revamped spell system
public class SpellList : MonoBehaviour
{
    [SerializeField] private List<BaseSpell> listOfSpells;



    public static SpellList Instance
    {
        get;
        private set;
    }


    private void Awake()
    {
        Debug.Log("SpellList Awake");

        if (Instance != null)
            Destroy(gameObject);

        else
        {
            Instance = this;
        }
    }


    private void OnDestroy()
    {
        Debug.Log("SpellList Destroyed");

        Instance = null;
    }


    public BaseSpell GetRandomSpell()
    {
        int randnum = Random.Range(0, listOfSpells.Count);

        return listOfSpells[randnum];
    }


    public BaseSpell GetTestSpell()
    {
        return listOfSpells[10];
    }
}