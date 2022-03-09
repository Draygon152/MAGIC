using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopAIBehavior : FriendlyBehaviorBase
{
    // Message to Lawson: One thing I recognize as I started to build the player ai script is that
    // it shares similar traits to my RangeBehavior script in my branch (UpgradeAttackingBehaviors).
    // I say take a look at it!

    [SerializeField] private const float MAX_LOOK_AT_ANGLE = 1.0f; //The max angle between the direction
                                                                   //of the target and the direction the AI
                                                                   //is looking can be for the AI to be consider
                                                                   //looking at the target 
    [SerializeField] private LayerMask environment;

    [SerializeField] private float healthCriticalTheshold = 0.2f; //The percentage that teammate's must drop below for the AI to assist
    [SerializeField] private float timeBetweenCheckingOnTeammate = 10.0f; //How often does the AI check how its teammate is doing
    private bool timeToCheckOnTeammate;
    private int otherPlayerIndex; //and integer to store the index of the other player for a multiplayer game


    private bool readyToScanEnemies; // A flag that indicates whether or not to scan enemies
    private Collider[] foundEnemies;
    private Collider[] foundObjects;

    private MagicCasting magic; //one's own magic

    private Transform selfTransform; //one's own transform

    private Transform target; //The current target to kill

    private enum CoopAiState
    {
        attackEnemies,
        defendPlayer,
    }
    private CoopAiState playerAiState;

    // Change this later when BehaviorBase has Awake() and Start()...
    private void Awake()
    {
        playerAiState = CoopAiState.attackEnemies;

        //Initiate the power of your own magic, 
        //so that it may tell you its strengths and limitations
        magic = this.gameObject.GetComponent<MagicCasting>();

        //start with no target
        target = null;

        //set own transform
        selfTransform = this.gameObject.transform;
    }

    protected override void Start()
    {
        base.Start();

        readyToScanEnemies = true;
        timeToCheckOnTeammate = true;

        //for a multiplayer game
        if (PlayerManager.Instance.GetNumberOfPlayers() == 2)
        {
            //get the index of the other player

            otherPlayerIndex = this.gameObject.GetComponent<Player>().PlayerNumber; //AI's player index

            //Converts to other player's index
            //if this is player 0, 0 + 1 is 1 and 1 % 2 is 1
            //if this is player 1, 1 + 1 is 2 and 2 % 2 is 0
            otherPlayerIndex = (otherPlayerIndex + 1) % PlayerManager.Instance.GetNumberOfPlayers();
        }
    }

    override protected void PerformBehavior()
    {
        base.PerformBehavior();

        // Grab current center position
        Vector3 playerAiCenter = selfTransform.position;

        //Scan for nearby enemies
        if (readyToScanEnemies)
        {
            StartCoroutine(ScanForEnemies(selfTransform.position));
        }

        switch (playerAiState)
        {
            case CoopAiState.attackEnemies:
                //Check for a target
                TargetNearestEnemy(selfTransform.position);

                //Check if the spell is on cooldown
                Debug.Log($"magic.GetTimeSinceLastCast {magic.GetTimeSinceLastCast() == 0}");
                if (magic.GetTimeSinceLastCast() == 0)
                {
                    //Spell is off cooldown, Charge into the fray

                    //The target must die
                    KillTheTarget();
                }
                else
                {
                    //Spell is on cooldown, flee for you life
                    if (target != null)
                    {
                        Flee(target.position);
                    }
                }

                // //Periodically check how the other player is doing
                // if (timeToCheckOnTeammate && PlayerManager.Instance.GetNumberOfPlayers() != 1)
                // {
                //     StartCoroutine(CheckOnTeammate());
                // }

                break;

            case CoopAiState.defendPlayer:
                //Target enemy nearest other player
                TargetNearestEnemy(PlayerManager.Instance.GetPlayerLocation(otherPlayerIndex).position);

                //Check if the spell is on cooldown
                if (magic.GetTimeSinceLastCast() == 0)
                {
                    //Spell is off cooldown, Charge into the fray

                    //The target must die
                    KillTheTarget();
                }
                else
                {
                    //Spell is on cooldown, focus on staying close to the other player

                    Follow(PlayerManager.Instance.GetPlayerLocation(otherPlayerIndex).position);
                }
                break;
        }
    }

    private void KillTheTarget()
    {
        Debug.Log("Killing the target");
        if (target == null)
        {
            //No target to kill, return broken hearted
            return;
        }

        Debug.Log("Target is not null");

        //Begin the attack
        if (TargetInRange())
        {
            Debug.Log("Target is in range");
            //No need to get closer
            agent.SetDestination(selfTransform.position);

            if (LookingAtTarget())
            {   
                Debug.Log("Looking at target");
                //Check the line of sight on the target
                if (CheckLineOfSight())
                {
                    //something is blocking the line of sight of the enemy
                    Follow(target.position);
                }
                else
                {
                    //Looking at target, Cast the spell and watch the target suffer
                    magic.AIOnCast();
                }
            }
            else
            {
                Debug.Log("Not looking at target");
                Debug.Log(target.position - selfTransform.position);
                //need to look at the target
                selfTransform.LookAt(target);
            }
        }
        else
        {
            Debug.Log("Target is not in range");
            //too far away
            //CHARGE FORWARD
            Follow(target.position);
        }
    }

    private bool TargetInRange()
    {
        return DistacneToTarget() <= magic.GetSpellRange();
    }

    private bool LookingAtTarget()
    {
        Vector3 lookDirection = selfTransform.forward;
        Vector3 directionOfTarget = target.position - selfTransform.position;
        return Vector3.Angle(directionOfTarget, lookDirection) <= MAX_LOOK_AT_ANGLE;
    }

    private float DistacneToTarget()
    {
        Vector3 distance = target.position - selfTransform.position;
        return distance.magnitude;
    }

    private bool CheckLineOfSight()
    {
        Debug.Log($"Line of sight {Physics.Raycast(selfTransform.position, selfTransform.forward, out RaycastHit hitInfoLog, magic.GetSpellRange(), environment)}");
        Debug.Log($"Raycast hit: {hitInfoLog.collider}");
        //use a raycast to see if an envornment object is in the way
        return Physics.Raycast(selfTransform.position, selfTransform.forward, out RaycastHit hitInfo, magic.GetSpellRange(), environment);
    }

    private IEnumerator ScanForEnemies(Vector3 playerAiCenter)
    {
        Debug.Log("Scanning for enemies");
        readyToScanEnemies = false;
        foundEnemies = DetectLayerWithinRadius(playerAiCenter, detectionRadiusBehavior, enemyLayerMask);

        yield return new WaitForSeconds(timeBetweenDetection);
        readyToScanEnemies = true;

        Debug.Log($"Found {foundEnemies.Length} enemies");
    }

    private IEnumerator ScanForObjects(Vector3 playerAiCenter)
    {
        readyToScanEnemies = false;
        foundObjects = DetectLayerWithinRadius(playerAiCenter, detectionRadiusBehavior, objectLayerMask);

        yield return new WaitForSeconds(timeBetweenDetection);
        readyToScanEnemies = true;
    }

    private IEnumerator CheckOnTeammate()
    {
        GameObject otherPlayer;

        timeToCheckOnTeammate = false;

        //check if other player's health is low
        otherPlayer = PlayerManager.Instance.GetPlayer(otherPlayerIndex).gameObject;
        if (otherPlayer.GetComponent<PlayerHealthManager>().HealthBelowPercentageThreshold(healthCriticalTheshold))
        {
            //The other player's health is low, assit them
            playerAiState = CoopAiState.defendPlayer;
        }

        yield return new WaitForSeconds(timeBetweenCheckingOnTeammate);
        timeToCheckOnTeammate = true;
    }

    //Target the nearest enemy to the sourcePoint
    private void TargetNearestEnemy(Vector3 sourcePoint)
    {
        Vector3 minDistance = Vector3.positiveInfinity; 
        Vector3 distanceToCurrentEnemy;

        if (foundEnemies != null)
        {
            foreach (Collider enemy in foundEnemies)
            {
                //enemy might be null if the enemy died since the last scan
                if (enemy != null)
                {
                    distanceToCurrentEnemy =  enemy.gameObject.transform.position - sourcePoint;

                    if (distanceToCurrentEnemy.magnitude < minDistance.magnitude)
                    {
                        //found a closer enemy, update target and min distance
                        target = enemy.gameObject.transform;
                        minDistance = distanceToCurrentEnemy;
                    }
                } //end if (enemy != null)
            } //end foreach (Collider enemy in foundEnemies)
        } //end if (foundEnemies != null)
    }
} 
