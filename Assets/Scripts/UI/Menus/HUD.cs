using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : Menu<HUD>
{
    public HealthBar Player1HealthBar;
    public HealthBar Player2HealthBar;



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
}