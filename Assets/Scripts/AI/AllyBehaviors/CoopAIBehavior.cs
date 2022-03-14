// Written by Lawson McCoy
// Modified by Lizbeth

using System.Collections;
using UnityEngine;

public class CoopAIBehavior : FriendlyBehaviorBase
{
    [SerializeField] private const float MAX_LOOK_AT_ANGLE = 5.0f; // The max angle between the direction
                                                                   // of the target and the direction the AI
                                                                   // is looking can be for the AI to be consider
                                                                   // looking at the target 
    [SerializeField] private LayerMask environment;

    // Variables related to checking on teammate
    [SerializeField] private float healthCriticalTheshold = 0.2f; // The percentage that teammate's must drop below for the AI to assist
    [SerializeField] private float timeBetweenCheckingOnTeammate = 10.0f; // How often does the AI check how its teammate is doing
    private bool timeToCheckOnTeammate;
    private int otherPlayerIndex; // and integer to store the index of the other player for a multiplayer game
    private HealthManager otherPlayerHealthManager;

    // Variables related to attacking enemies
    [SerializeField] private float placeDistanceForStationarySpells = 3.0f; // How far away from the enemy should a stationary spell be placed
    [SerializeField] private float minAttackingDistance = 1.0f;
    [SerializeField] private float safeDistanceToBeginAttackingAgain = 5.0f;
    private bool readyToScanEnemies; // A flag that indicates whether or not to scan enemies
    private Collider[] foundEnemies;
    private Collider[] foundObjects;

    private MagicCasting magic; // one's own magic
    private Transform selfTransform; // one's own transform
    private Transform target; // The current target to kill

    private enum CoopAIState
    {
        attackEnemies,
        fleeFromNearbyEnemy,
        defendPlayer,
    }
    private CoopAIState playerAIState;



    protected override void Awake()
    {
        base.Awake();

        playerAIState = CoopAIState.attackEnemies;
        magic = this.gameObject.GetComponent<MagicCasting>();
        target = null;
        selfTransform = this.gameObject.transform;
    }


    private void Start()
    {
        readyToScanEnemies = true;
        timeToCheckOnTeammate = true;

        // for a multiplayer game
        if (PlayerManager.Instance.GetNumberOfPlayers() == 2)
        {
            // get the index of the other player
            otherPlayerIndex = this.gameObject.GetComponent<Player>().PlayerNumber; // AI's player index

            // Converts to other player's index
            // if this is player 0, 0 + 1 is 1 and 1 % 2 is 1
            // if this is player 1, 1 + 1 is 2 and 2 % 2 is 0
            otherPlayerIndex = (otherPlayerIndex + 1) % PlayerManager.Instance.GetNumberOfPlayers();
            otherPlayerHealthManager = PlayerManager.Instance.GetPlayer(otherPlayerIndex).GetComponent<HealthManager>();
        }
    }


    protected override void PerformBehavior()
    {
        base.PerformBehavior();

        // Grab current center position
        Vector3 playerAiCenter = selfTransform.position;

        // Scan for nearby enemies
        if (readyToScanEnemies)
        {
            StartCoroutine(ScanForEnemies(selfTransform.position));
        }

        switch (playerAIState)
        {
            case CoopAIState.attackEnemies:
                TargetNearestEnemy(selfTransform.position);

                if (target != null)
                {
                    // Debug.Log($"magic.GetTimeSinceLastCast {magic.GetTimeSinceLastCast() == 0}");

                    // If spell is off cooldown, attack
                    if (magic.GetTimeSinceLastCast() == 0)
                    {
                        KillTheTarget();
                    }

                    // If spell is on cooldown, flee
                    else
                    {                    
                        Flee(target.position);
                    }

                    // If target is too close, gain distance from enemy
                    if (DistanceToTarget() < minAttackingDistance)
                    {
                        playerAIState = CoopAIState.fleeFromNearbyEnemy;
                    }

                    // Periodically check how the other player is doing
                    if (timeToCheckOnTeammate && PlayerManager.Instance.GetNumberOfPlayers() != 1)
                    {
                        StartCoroutine(CheckOnTeammate());
                    }
                }

                break;


            case CoopAIState.fleeFromNearbyEnemy:
                TargetNearestEnemy(selfTransform.position);

                // If no enemies are in range or safe distance reached, safe to attack again
                if (target == null || DistanceToTarget() > safeDistanceToBeginAttackingAgain)
                {
                    playerAIState = CoopAIState.attackEnemies;
                }

                // Otherwise, continue fleeing
                else
                {
                    Flee(target.position);
                }

                break;


            case CoopAIState.defendPlayer:
                // Check if teammate is dead or healed
                if (PlayerManager.Instance.GetDeadPlayer() == null && otherPlayerHealthManager.HealthBelowPercentageThreshold(healthCriticalTheshold))
                {
                    // Target enemy nearest other player
                    TargetNearestEnemy(PlayerManager.Instance.GetPlayerLocation(otherPlayerIndex).position);

                    // If spell is off cooldown and there is a target, attack target
                    if (magic.GetTimeSinceLastCast() == 0 && target != null)
                    {
                        KillTheTarget();
                    }

                    // Otherwise, follow teammate
                    else
                    {
                        // Debug.Log("Going to teammate");

                        Follow(PlayerManager.Instance.GetPlayerLocation(otherPlayerIndex).position);
                    }
                }

                // If teammate is alive or above the critical health threshold, return to attack state
                else
                {
                    playerAIState = CoopAIState.attackEnemies;
                }

                break;
        }
    }


    private void KillTheTarget()
    {
        // Debug.Log("Target is not null");

        // Begin the attack
        if (TargetInRange())
        {
            // Debug.Log("Target is in range");

            // No need to get closer
            agent.SetDestination(selfTransform.position);

            if (LookingAtTarget())
            {
                // Debug.Log("Looking at target");

                // If something is blocking the line of sight of the enemy, move to enemy
                if (CheckLineOfSight())
                {
                    Follow(target.position);
                }

                // Otherwise, if line of sight achieved, cast magic
                else
                {
                    magic.AIOnCast();
                }
            }

            // AI is not looking at target, turn to face target
            else
            {
                // Debug.Log("Not looking at target");
                // Debug.Log(target.position - selfTransform.position);

                selfTransform.LookAt(target);
            }
        }

        // If target is not in range, approach target
        else
        {
            // Debug.Log("Target is not in range");

            Follow(target.position);
        }
    }


    private bool TargetInRange()
    {
        bool inRange;

        if (magic.GetSpellRange() == -1)
        {
            inRange = DistanceToTarget() <= placeDistanceForStationarySpells;
        }

        else
        {
            inRange = DistanceToTarget() <= magic.GetSpellRange();
        }

        return inRange;
    }


    private bool LookingAtTarget()
    {
        Vector3 lookDirection = selfTransform.forward;
        Vector3 directionOfTarget = target.position - selfTransform.position;

        return Vector3.Angle(directionOfTarget, lookDirection) <= MAX_LOOK_AT_ANGLE;
    }


    private float DistanceToTarget()
    {
        Vector3 distance = target.position - selfTransform.position;

        return distance.magnitude;
    }


    private bool CheckLineOfSight()
    {
        // Debug.Log($"Line of sight {Physics.Raycast(selfTransform.position, selfTransform.forward, out RaycastHit hitInfoLog, magic.GetSpellRange(), environment)}");
        // Debug.Log($"Raycast hit: {hitInfoLog.collider}");

        // use a raycast to see if an envornment object is in the way
        return Physics.Raycast(selfTransform.position, selfTransform.forward, out RaycastHit hitInfo, magic.GetSpellRange(), environment);
    }


    private IEnumerator ScanForEnemies(Vector3 playerAiCenter)
    {
        // Debug.Log("Scanning for enemies");
        readyToScanEnemies = false;
        foundEnemies = DetectLayerWithinRadius(playerAiCenter, detectionRadiusBehavior, enemyLayerMask);

        yield return new WaitForSeconds(timeBetweenDetection);
        readyToScanEnemies = true;

        // Debug.Log($"Found {foundEnemies.Length} enemies");
    }


    private IEnumerator ScanForObjects(Vector3 playerAiCenter)
    {
        readyToScanEnemies = false;
        foundObjects = DetectLayerWithinRadius(playerAiCenter, detectionRadiusBehavior, objectLayerMask);

        yield return new WaitForSeconds(timeBetweenDetection);
        readyToScanEnemies = true;
    }


    // Future builds could use the EventManager to notify the Coop AI that the human player is low
    // on health instead of conducting a periodic check.
    private IEnumerator CheckOnTeammate()
    {
        timeToCheckOnTeammate = false;

        // check if other player's health is low
        if (otherPlayerHealthManager.HealthBelowPercentageThreshold(healthCriticalTheshold))
        {
            // The other player's health is low, assist them
            playerAIState = CoopAIState.defendPlayer;
        }

        yield return new WaitForSeconds(timeBetweenCheckingOnTeammate);
        timeToCheckOnTeammate = true;
    }


    // Target the nearest enemy to the sourcePoint
    private void TargetNearestEnemy(Vector3 sourcePoint)
    {
        Vector3 minDistance = Vector3.positiveInfinity; 

        if (foundEnemies != null)
        {
            foreach (Collider enemy in foundEnemies)
            {
                // enemy might be null if the enemy died since the last scan
                if (enemy != null)
                {
                    Vector3 distanceToCurrentEnemy =  enemy.gameObject.transform.position - sourcePoint;

                    if (distanceToCurrentEnemy.magnitude < minDistance.magnitude)
                    {
                        // found a closer enemy, update target and min distance
                        target = enemy.gameObject.transform;
                        minDistance = distanceToCurrentEnemy;
                    }
                }
            }
        }
    }
}