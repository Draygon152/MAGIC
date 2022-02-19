using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    [SerializeField] List<BaseSpell> listofSpells;

    public BaseSpell spellRandomizer()
    {
        int spellListnum = listofSpells.Count;
        int randnum = Random.Range(0,spellListnum);
        return listofSpells[randnum];
    }

}
