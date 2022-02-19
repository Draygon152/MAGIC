// Written by Kevin Chao
// Modified by Angel

public class HUD : Menu<HUD>
{
    public HealthBar Player1HealthBar;
    public HealthBar Player2HealthBar;
    public SelectedSpellUI Player1SpellUI;
    public SelectedSpellUI Player2SpellUI;



    public void SetP1CurHealth(int newHealth)
    {
        Player1HealthBar.SetCurHealth(newHealth);
    }


    public void SetP1MaxHealth(int maxHealth)
    {
        Player1HealthBar.SetMaxHealth(maxHealth);
    }


    public void SetP2CurHealth(int newHealth)
    {
        Player2HealthBar.SetCurHealth(newHealth);
    }


    public void SetP2MaxHealth(int maxHealth)
    {
        Player2HealthBar.SetMaxHealth(maxHealth);
    }


    public void SetP1SpellCaster(MagicCasting caster)
    {
        Player1SpellUI.InitializeSpellUI(caster);
    }


    public void SetP2SpellCaster(MagicCasting caster)
    {
        Player2SpellUI.InitializeSpellUI(caster);
    }
}