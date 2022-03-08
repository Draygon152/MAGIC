// Written by Kevin Chao
// Modified by Lizbeth A

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



    private void Awake()
    {
        magicCaster = GetComponent<MagicCasting>();
        healthManager = GetComponent<PlayerHealthManager>();
    }


    public void SetElement(Element elem)
    {
        magicCaster.InitializeSpell(elem);
    }


    public MagicCasting GetCaster()
    {
        return magicCaster;
    }


    public void SetHealthBarDelegates(Action<int, int> setHealthBarValue, Action<int, int> setHealthBarMax)
    {
        healthManager.SetHealthBarDelegates(playerNumber, setHealthBarValue, setHealthBarMax);
    }
}