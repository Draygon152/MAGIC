// Written by Kevin Chao

using UnityEngine;
using System;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealthManager))]
[RequireComponent(typeof(MagicCasting))]
public class Player : MonoBehaviour
{
    private MagicCasting magicCaster;
    private PlayerHealthManager healthManager;



    private void Awake()
    {
        magicCaster = GetComponent<MagicCasting>();
        healthManager = GetComponent<PlayerHealthManager>();
    }


    public void SetElement(Element elem)
    {
        magicCaster.InitializeSpell(elem);
    }


    public BaseSpell GetBaseSpell()
    {
        return magicCaster.ReturnSpell();
    }


    public void SetHealthBarDelegates(Action<int> setHealthBarValue, Action<int> setHealthBarMax)
    {
        healthManager.SetHealthBarDelegates(setHealthBarValue, setHealthBarMax);
    }
}