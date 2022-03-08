using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopAIBehavior : FriendlyBehaviorBase
{
    // Message to Lawson: One thing I recognize as I started to build the player ai script is that
    // it shares similar traits to my RangeBehavior script in my branch (UpgradeAttackingBehaviors).
    // I say take a look at it!


    [SerializeField] private float stopAtDistance = 15f; // Player AI will stop at a certain distance

    private bool readyToScanEnemies; // A flag that indicates whether or not to scan enemies
    private Collider[] foundEnemies;
    private Collider[] foundObjects;

    private MagicCasting magic; //one's own magic

    //sorry kevin, I just want to get this working first then I will go back and 
    //change the data type 
    GameObject target;


    private enum CoopAiState
    {
        attackEnemies,
        defendPlayer,
        seekSpells
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
    }

    private void Start()
    {
        agent.stoppingDistance = stopAtDistance;
        readyToScanEnemies = true;
    }

    override protected void PerformBehavior()
    {
        base.PerformBehavior();

        // Grab current center position
        Vector3 playerAiCenter = this.gameObject.transform.position;

        switch (playerAiState)
        {
            case CoopAiState.attackEnemies:

                //Scan for nearby enemies
                if (readyToScanEnemies)
                {
                    StartCoroutine(ScanForEnemies(this.gameObject.transform.position));
                }

                //Check for a target
                TargetNearestEnemy();

                //The target must die
                KillTheTarget();

                break;

            case CoopAiState.defendPlayer:
                break;

            case CoopAiState.seekSpells:
                break;
        }
    }

    private void KillTheTarget()
    {
        if (target == null)
        {
            //No target to kill, return broken hearted
            return;
        }

        //Begin the attack
        if (TargetInRange())
        {
            //No need to get closer
            agent.ResetPath();

            //Cast the spell and watch the target suffer
            magic.AIOnCast();
        }
        else
        {
            //too far away
            //CHARGE FORWARD
            Follow(target.gameObject.transform.position);
        }
    }

    private bool TargetInRange()
    {
        return DistacneToTarget() <= magic.GetSpellRange();
    }

    private float DistacneToTarget()
    {
        Vector3 distance = target.gameObject.transform.position - this.gameObject.transform.position;
        return distance.magnitude;
    }

    private IEnumerator ScanForEnemies(Vector3 playerAiCenter)
    {
        readyToScanEnemies = false;
        foundEnemies = DetectLayerWithinRadius(playerAiCenter, detectionRadiusBehavior, enemyLayerMask);

        yield return new WaitForSeconds(timeBetweenDetection);
        readyToScanEnemies = true;
    }

    private IEnumerator ScanForObjects(Vector3 playerAiCenter)
    {
        readyToScanEnemies = false;
        foundObjects = DetectLayerWithinRadius(playerAiCenter, detectionRadiusBehavior, objectLayerMask);

        yield return new WaitForSeconds(timeBetweenDetection);
        readyToScanEnemies = true;
    }

    private void TargetNearestEnemy()
    {
        Vector3 minDistance = Vector3.positiveInfinity; 
        Vector3 distanceToCurrentEnemy;

        foreach (Collider enemy in foundEnemies)
        {
            distanceToCurrentEnemy =  enemy.gameObject.transform.position - this.gameObject.transform.position;

            if (distanceToCurrentEnemy.magnitude < minDistance.magnitude)
            {
                //found a closer enemy, update target and min distance
                target = enemy.gameObject;
                minDistance = distanceToCurrentEnemy;
            }
        } //end foreach (Collider enemy in foundEnemies)
    }
} 
