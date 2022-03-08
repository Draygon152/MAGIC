// Written by Lizbeth
// Modified by Lawson and Angel

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

// This class serve as the base for all enemies. Each has the ability to follow/flee, wonder, and attack within radius
public class EnemyBehaviorBase : MonoBehaviour
{
    // Advanced following variables
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private float detectionRadius; // Enemy's detection radius
    [SerializeField] private float timeBetweenCheckPlayers; // How many seconds should the enemy check for players
    [SerializeField] private LayerMask layerMask; // Detect colliders within layerMask

    // Flee variables
    [SerializeField] protected bool hasFleeBehavior; // flag which indicates the enemy will flee at some distance
    [SerializeField] protected float fleeCooldown = 5f;
    [SerializeField] protected float fleeDistance; // Enemy runs away if player is within fleeDistance

    // Wander variables
    [SerializeField] private float wanderMinRadius;
    [SerializeField] private float wanderMaxRadius;
    [SerializeField] private float timeBetweenWander; // How many seconds should the enemy wander
    [SerializeField] private float wanderTowardsPlayerInterval;
    [SerializeField] private float charge; // Increase the charge temporarily when enemy cannot find player for a while

    // Attack variables
    [SerializeField] protected float attackDistance = 1; // Distance betweeen the enemy itself and target

    protected PlayerManager playerManager;
    protected Collider[] foundPlayers; // List of players' colliders
    protected int currentTargetNumber; // Enemy's current player target and -1 represents no target
    private float wanderInterval;
    private float enemyOriginalSpeed;
    private bool checkForPlayers;
    private bool isWanderTime;
    private bool gameOver; // If false, behavior will execute. Set to true when a game ends to prevent
                           // minions from causing a game end after a player wins



    protected virtual void Awake()
    {
        EventManager.Instance.Subscribe(EventTypes.Events.GameOver, DisableBehavior);
        gameOver = false;
    }


    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe(EventTypes.Events.GameOver, DisableBehavior);
    }


    // Initializes enemy's agent
    protected virtual void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerManager = PlayerManager.Instance;

        currentTargetNumber = -1;
        wanderInterval = 0;
        enemyOriginalSpeed = agent.speed;

        checkForPlayers = true;
        isWanderTime = true;
    }


    private void FixedUpdate()
    {
        if (!gameOver)
        {
            // Checks for players in radius and waits a while before checking again.
            if (checkForPlayers)
            {
                StartCoroutine(FindPlayersWithinRadius());
            }

            // If there is a current player target, enemy would proceed to follow the chosen player
            // If not, enemy would begin to wander
            if (currentTargetNumber != -1)
            {
                // Perform the behavior for this enemy, the base function will just follow, but it
                // can be overriden for different behaviors
                PerformBehavior();
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


    // Enemy follows to assigned location
    protected void Follow(Vector3 targetLocation)
    {
        agent.SetDestination(targetLocation);
    }


    protected void Flee(Vector3 location)
    {
        Vector3 distance = location - this.gameObject.transform.position;
        Vector3 fleeVector = this.transform.position - distance;

        NavMeshHit hit;
        bool validLocation = NavMesh.SamplePosition(fleeVector, out hit, 10.0f, NavMesh.AllAreas);


        if (validLocation)
        {
            agent.SetDestination(fleeVector);
        }
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


    // WanderEnemyFindClosestTarget() finds the closest player within distance
    private Vector3 WanderEnemyFindClosestTarget()
    {
        Player[] playerList = playerManager.GetFullPlayerList();
        Vector3 shorterPosition = Vector3.positiveInfinity;
        foreach (Player player in playerList)
        {
            float currentDistance = Vector3.Distance(this.transform.position, shorterPosition);
            Transform playerLocation = playerManager.GetPlayerLocation(player.PlayerNumber);
            float newDistance = Vector3.Distance(this.transform.position, playerLocation.position);

            if (newDistance <= currentDistance && player.gameObject.activeInHierarchy)
            {
                shorterPosition = playerLocation.position; // Found shortest position
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

            if (newDistance <= currentDistance && player.gameObject.activeInHierarchy)
            {
                shorterPosition = playerManager.GetPlayerLocation(playerNum).position; // Found shortest position
                currentTargetNumber = playerNum; // Set enemy's target
            }
        }
    }


    // Returns a boolean that states whether current target is within attack distance
    protected bool IsWithinAttackDistance()
    {
        bool isWithinRadius = false;

        // Checks if there is a current target. If not, then it is not within attack distance
        if (currentTargetNumber != -1)
        {
            Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
            float distance = Vector3.Distance(this.transform.position, targetLocation);

            if (distance <= attackDistance)
            {
                isWithinRadius = true;
            }
        }

        return isWithinRadius;
    }


    // Returns a boolean that states whether current target is within flee distance
    // If enemy does not have flee behavior enabled, this function automatically returns false
    protected bool IsWithinFleeDistance()
    {
        bool isWithinDistance = false;

        if (hasFleeBehavior && currentTargetNumber != -1)
        {
            Vector3 targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
            float distance = Vector3.Distance(this.transform.position, targetLocation);

            if (distance <= fleeDistance)
            {
                isWithinDistance = true;
            }
        }

        return isWithinDistance;
    }


    protected virtual void PerformBehavior()
    {
        if (agent.speed != enemyOriginalSpeed)
        {
            agent.speed = enemyOriginalSpeed;
        }
    }


    public void ChangeSpeed(float newspeed)
    {
        enemyOriginalSpeed = newspeed;
    }


    public float ReturnSpeed()
    {
        return agent.speed;
    }
    
    private void DisableBehavior()
    {
        gameOver = true;
    }
}