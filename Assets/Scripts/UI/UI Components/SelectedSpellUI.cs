// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.UI;

public class SelectedSpellUI : MonoBehaviour
{
    [SerializeField] private Text selectedSpellText;
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private MagicCasting playerCastSystem;



    private void Update()
    {
        if (cooldownSlider != null)
        {
            cooldownSlider.value = playerCastSystem.GetTimeSinceLastCast();
        }
    }


    public void InitializeSpellUI(MagicCasting caster)
    {
        selectedSpellText.text = caster.GetSpell().ToString();

        cooldownSlider.maxValue = caster.GetSpell().timeBetweenCasts;
        cooldownSlider.value = 0.0f;
    }


    public void ChangeSpellCooldown(float newCooldown)
    {
        cooldownSlider.maxValue = newCooldown;
    }


    public float CheckSpellCooldownValue()
    {
        return cooldownSlider.value;
    }
}