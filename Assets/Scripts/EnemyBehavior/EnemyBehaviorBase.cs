// Written by Liz

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

// This class currently set the enemy's basic behavior to follow the nearest player within radius
public class EnemyBehaviorBase : MonoBehaviour
{
    // Advanced following variables
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private float detectionRadius; // Enemy's detection radius
    [SerializeField] private float timeBetweenCheckPlayers; // How many seconds should the enemy check for players
    [SerializeField] private LayerMask layerMask; // Detect colliders within layerMask
    // Wander variables
    [SerializeField] private float wanderMinRadius;
    [SerializeField] private float wanderMaxRadius;
    [SerializeField] private float timeBetweenWander; // How many seconds should the enemy wander
    [SerializeField] private float wanderTowardsPlayerInterval;
    [SerializeField] private float charge; // Increase the charge temporarily when enemy cannot find player for a while

    protected PlayerManager playerManager;
    protected Collider[] foundPlayers; // List of players' colliders
    protected int currentTargetNumber; // Enemy's current player target and -1 represents no target
    private float wanderInterval;
    private float enemyOriginalSpeed;
    private bool checkForPlayers;
    private bool isWanderTime;
    

    // Initializes enemy's agent
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerManager = PlayerManager.Instance;
        currentTargetNumber = -1;
        wanderInterval = 0;
        enemyOriginalSpeed = agent.speed;
        checkForPlayers = true;
        isWanderTime = true;
}

    // Enemy follows to assigned location
    private void Follow(Vector3 targetLocation)
    {
        agent.SetDestination(targetLocation);
    }

    private IEnumerator Wander()
    {
        isWanderTime = false;
        Vector3 wanderTarget = Vector3.zero;
        // Time for the enemy to be smart
        if (wanderInterval >= wanderTowardsPlayerInterval)
        {
            // Charge towards the last known location a player was at
            wanderTarget = WanderEnemyFindClosestTarget();
            wanderInterval = 0;
            agent.speed *= charge;
        }
        else
        { 
            // Select a wander target at random around the enemy
            Vector2 point = Random.insideUnitCircle.normalized * Random.Range(wanderMinRadius, wanderMaxRadius);
            wanderTarget = new Vector3(point.x, 0, point.y) + this.transform.position;
            wanderInterval++;
        }
        Follow(wanderTarget); // Move enemy to wander target

        yield return new WaitForSeconds(timeBetweenWander);
        agent.speed = enemyOriginalSpeed;
        isWanderTime = true;
    }


    // WanderEnemyFindClosestTarget() finds the closest player desite distance
    private Vector3 WanderEnemyFindClosestTarget()
    {
        Player[] playerList = playerManager.GetFullPlayerList();
        Vector3 shorterPosition = Vector3.positiveInfinity;
        foreach (Player player in playerList)
        {
            float currentDistance = Vector3.Distance(this.transform.position, shorterPosition);
            Transform playerLocation = playerManager.GetPlayerLocation(player.PlayerNumber);
            float newDistance = Vector3.Distance(this.transform.position, playerLocation.position);
            if (newDistance <= currentDistance)
            {
                shorterPosition = playerLocation.position; // Found shorteset position
            }
        }
        return shorterPosition;
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
        else
        {
            currentTargetNumber = -1; // No target
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
        //IMPLEMENT PURSUE :V
        // If there is a current player target, enemy would process to follow the chosen player
        // If not, enemy would begin to wander
        if (currentTargetNumber != -1)
        {
            if (agent.speed != enemyOriginalSpeed)
            {
                agent.speed = enemyOriginalSpeed;
            }
            Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position; // Grab targeted player's location
            Follow(targetLocation);
        }
        else
        {
            if (isWanderTime)
            { 
                StartCoroutine(Wander());
            }
        }
    }
}
