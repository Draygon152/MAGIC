// Written by Angel
// Modified by Kevin Chao

using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    [SerializeField] private List<BaseSpell> listOfSpells;



    public BaseSpell spellRandomizer()
    {
        int randnum = Random.Range(0, listOfSpells.Count);

        return listOfSpells[randnum];
    }
}