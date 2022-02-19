// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

public class SelectedSpellUI : MonoBehaviour
{
    [SerializeField] private Text selectedSpellText;
    [SerializeField] private Slider cooldownSlider;

    private MagicCasting playerCastSystem;



    private void Awake()
    {
        selectedSpellText.text = "Testing SpellUI";
    }


    private void Update()
    {
        if (cooldownSlider != null)
            cooldownSlider.value = playerCastSystem.GetTimeSinceLastCast();
    }


    public void InitializeSpellUI(MagicCasting caster)
    {
        selectedSpellText.text = caster.ReturnSpell().ToString();
        cooldownSlider.maxValue = caster.ReturnSpell().spellToCast.timeBetweenCasts;
        cooldownSlider.value = 0.0f;

        playerCastSystem = caster;
    }
}