// Written by Kevin Chao
// Modified by Lizbeth

using UnityEngine;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealthManager))]
[RequireComponent(typeof(MagicCasting))]
[RequireComponent(typeof(NavMeshAgent))] // for AI COOP
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // The static stats of the player

    private MagicCasting magicCaster;
    private PlayerHealthManager healthManager;
    private NavMeshAgent navMeshAgent;
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
        navMeshAgent = GetComponent<NavMeshAgent>();

        // initialize approriate fields with player stats
        navMeshAgent.speed = stats.speed;
        navMeshAgent.angularSpeed = stats.turnSpeed;
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