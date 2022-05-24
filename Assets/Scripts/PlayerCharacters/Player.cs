// Written by Kevin Chao
// Modified by Lizbeth

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealthManager))]
[RequireComponent(typeof(MagicCasting))]
[RequireComponent(typeof(NavMeshAgent))] // for co-op AI
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // The static stats of the player
    [SerializeField] private Image gemColor;
    [SerializeField] private TMP_Text playerID;

    private MagicCasting magicCaster;
    private SelectedSpellUI spellUI;

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
            playerID.text = $"Player {playerNumber + 1}";
        }
    }



    private void Awake()
    {
        magicCaster = GetComponent<MagicCasting>();
        spellUI = GetComponentInChildren<SelectedSpellUI>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // initialize approriate fields with player stats
        navMeshAgent.speed = stats.speed;
        navMeshAgent.angularSpeed = stats.turnSpeed;
    }


    public void SetElement(Element elem)
    {
        magicCaster.InitializeSpell(elem);
        gemColor.color = elem.GetElementColor();
        spellUI.InitializeSpellUI(magicCaster);
    }


    public MagicCasting GetCaster()
    {
        return magicCaster;
    }
}