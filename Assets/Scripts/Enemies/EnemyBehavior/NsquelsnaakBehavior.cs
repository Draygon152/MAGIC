// Written by Lawson

using System.Collections;
using UnityEngine;

public class NsquelsnaakBehavior : EnemyBehaviorBase
{
    [SerializeField] private float distanceToMaintain = 15.0f;   // The distance the Nsquelsnaak will maintain between itself and the player
    [SerializeField] private float maintainDistanceError = 5.0f; // The error from the correct maintain distance that is allowed

    [SerializeField] private EnemyBehaviorBase larvaPrefab; // The prefab for spawning the larvae
    [SerializeField] private int spawnTime = 3;             // The time between spawning of larvae
    [SerializeField] private int spawnAmount = 3;           // The amount of larvae that will spawn each time
    [SerializeField] private float spawnRange = 3.0f;       // The maximum range for spawning the larvae

    private Vector3 targetLocation;   // A variable for storing the target's location 
    private float distanceFromTarget; // A float to store the distance between the enemy and the target

    private bool spawningLarva = false; // A bool to mark if the Nsquelsnaak is currently spawning larvae


    // An enum for keeping track of states for maintaining a certain distance
    private enum MaintainDistanceState
    {
        moveTowardPlayer,
        moveAwayFromPlayer,
        idle
    }
    private MaintainDistanceState state;



    protected override void PerformBehavior()
    {
        // Get the distance from the target
        targetLocation = playerManager.GetPlayerLocation(currentTargetNumber).position;
        distanceFromTarget = (this.gameObject.transform.position - targetLocation).magnitude;

        switch (state)
        {
            case MaintainDistanceState.moveTowardPlayer:
                // Follow the player to get closer
                Follow(targetLocation);

                // Stop if at the desired distance
                if (distanceFromTarget <= distanceToMaintain)
                {
                    state = MaintainDistanceState.idle;
                }
                break;

            case MaintainDistanceState.moveAwayFromPlayer:
                // Flee from the player to get farther away
                Flee(targetLocation);

                // Stop if at the desired distance
                if (distanceFromTarget >= distanceToMaintain)
                {
                    state = MaintainDistanceState.idle;
                }
                break;

            case MaintainDistanceState.idle:
                // Do nothing but the distance check

                // Check if outside the error range
                if (distanceFromTarget > distanceFromTarget + maintainDistanceError)
                {
                    // too far away, get closer
                    state = MaintainDistanceState.moveTowardPlayer;
                }
                else if (distanceToMaintain < distanceFromTarget - maintainDistanceError)
                {
                    // too close, get farther away
                    state = MaintainDistanceState.moveAwayFromPlayer;
                }
                break;
        } // end switch (state)

        // Nsquelsnaak action
        // spawn in larva
        if (!spawningLarva)
        {
            StartCoroutine(SpawnLarva());
        }
    } // end PerformBehavior


    private IEnumerator SpawnLarva()
    {
        float randomRotation;  // A variable to store a random spawning direction
        float randomMagnitude; // A variable to store a random spawning distance
        Vector3 spawnPoint;    // A vector to store the larva's spawn point 

        // mark the Nsquelsnaak as spawning larva
        spawningLarva = true;

        // A loop to spawn the correct amount of larva
        for (int index = 0; index < spawnAmount; index++)
        {
            // get random values
            randomRotation = Random.Range(0, 360);
            randomMagnitude = Random.Range(0, spawnRange);

            // Get relative spawn point
            // Starting with the forward vector (1, 0, 0)
            // rotate randomRotation degrees around the up vector (0, 1, 0)
            // then scale by random magnitude
            spawnPoint = (Quaternion.AngleAxis(randomRotation, Vector3.up) * Vector3.forward) * randomMagnitude;

            // Convert to world coordinates of the spawn point
            spawnPoint = this.gameObject.transform.position + spawnPoint;

            // Instantiate the larva
            Instantiate(larvaPrefab, spawnPoint, this.gameObject.transform.rotation);
        }// end for (int index = 0; index < spawnAmount; index++)

        // Wait for cooldown then mark as not spawning
        yield return new WaitForSeconds(spawnTime);
        spawningLarva = false;
    }
}