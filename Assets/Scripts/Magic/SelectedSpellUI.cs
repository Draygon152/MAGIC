//Worked on by Angel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSpellUI : MonoBehaviour
{
    [SerializeField] private Text SelectedSpellText;

    void awake()
    {
        SelectedSpellText.text = $"Testing SpellUI";
    }

    public void changeSpellText(BaseSpell spellInfo)
    {
        string stringSpellInfo = spellInfo.ToString();
        SelectedSpellText.text = stringSpellInfo;
    }
}
