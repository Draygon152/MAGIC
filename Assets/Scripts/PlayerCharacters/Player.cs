// Written by Kevin Chao
// Little Modification by Lizbeth A

using UnityEngine;
using System;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealthManager))]
[RequireComponent(typeof(MagicCasting))]
public class Player : MonoBehaviour
{
    private MagicCasting magicCaster;
    private PlayerHealthManager healthManager;
    private int playerNumber;

    private void Awake()
    {
        magicCaster = GetComponent<MagicCasting>();
        healthManager = GetComponent<PlayerHealthManager>();
    }


    public void SetElement(Element elem)
    {
        magicCaster.InitializeSpell(elem);
    }

    // Liz's modification
    // Grabs Player game object's number
    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }
        set 
        {
            playerNumber = value;
        }
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