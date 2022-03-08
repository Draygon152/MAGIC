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
    Collider[] foundEnemies;
    Collider[] foundObjects;


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

                break;

            case CoopAiState.defendPlayer:
                break;

            case CoopAiState.seekSpells:
                break;
        }
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
}
