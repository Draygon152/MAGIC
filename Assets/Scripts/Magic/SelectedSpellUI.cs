// Written by Angel Rubio

using UnityEngine;
using UnityEngine.UI;

public class SelectedSpellUI : MonoBehaviour
{
    [SerializeField] private Text SelectedSpellText;



    private void Awake()
    {
        SelectedSpellText.text = $"Testing SpellUI";
    }


    public void changeSpellText(BaseSpell spellInfo)
    {
        string stringSpellInfo = spellInfo.ToString();
        SelectedSpellText.text = stringSpellInfo;
    }
}
