// Written by Liz

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

// This class currently set the enemy's basic behavior to follow the nearest player within radius
public class EnemyBehaviorBase : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private float detectionRadius; // Enemy's detection radius
    [SerializeField] private LayerMask layerMask; // Detect colliders within layerMask
    [SerializeField] private float timeBetweenCheckPlayers; // How many seconds should the enemy check for players

    protected PlayerManager playerManager = PlayerManager.Instance;
    private bool checkForPlayers = true;
    protected Collider[] foundPlayers; // List of players' colliders
    protected int currentTargetNumber = -1; // Enemy's current player target. -1 represents null

    // Initializes enemy's agent
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Enemy follows to assigned location
    protected void Follow(Vector3 location)
    {
        agent.SetDestination(location);
    }
    
    
    // Checks for Player Objects within radius
     private IEnumerator FindPlayersWithinRadius()
    {
        checkForPlayers = false;
        Vector3 enemyCenter = this.gameObject.transform.position;
        foundPlayers = Physics.OverlapSphere(enemyCenter, detectionRadius, layerMask); // Enemy's detection radius within layerMask

        // If player(s) are nearby, set the closest player to the enemy's target.
        if (foundPlayers.Length != 0)
        {
            TargetNearestPlayer(); 
        }

        yield return new WaitForSeconds(timeBetweenCheckPlayers);
        checkForPlayers = true;
    }
    
    // Assign enemy's target to the nearst player detected
    private void TargetNearestPlayer()
    {
        Vector3 shorterPosition = Vector3.positiveInfinity; 
        foreach (Collider player in foundPlayers)
        {
            Player currentPlayer = player.gameObject.GetComponent<Player>();
            int playerNum = currentPlayer.PlayerNumber;

            float currentDistance = Vector3.Distance(this.transform.position, shorterPosition);
            float newDistance = Vector3.Distance(this.transform.position, playerManager.GetPlayerLocation(playerNum).position);
            if (newDistance <= currentDistance)
            {
                shorterPosition = playerManager.GetPlayerLocation(playerNum).position; // Found shorteset position
                currentTargetNumber = playerNum; // Set enemy's target
            }
        }
    }
 
    // Update is called once per frame
    private void FixedUpdate()
    {
        // Checks for players in radius and waits a while before checking again.
        if (checkForPlayers)
        {
            StartCoroutine(FindPlayersWithinRadius());
        }

        // If there is a current player target, enemy would process to follow the chosen player
        // Target will change if found a closer player
        if (currentTargetNumber != -1)
        { 
            Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position; // Grab targeted player's location
            Follow(targetLocation);
        }
    }

    protected void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.gameObject.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }
}
