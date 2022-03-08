// Written by Lawson

using UnityEngine;

// This script would need to be adjusted later
public abstract class BehaviorBase : MonoBehaviour
{
    // Detection Variables
    // Change detectionRadiusBehavior to detectionRadius after my (Liz) new enemy branch is merged
    [SerializeField] protected float detectionRadiusBehavior; // AI's detection radius
    [SerializeField] protected float timeBetweenDetection;
    [SerializeField] protected LayerMask enemyLayerMask; // Detect enemies layer
    [SerializeField] protected LayerMask objectLayerMask; // Detect objects layer
    [SerializeField] protected LayerMask playerLayerMask; // Detect players layer

    //Performs this behavior every frame
    private void FixedUpdate()
    {
        PerformBehavior();
    }


    //The behavior of the AI
    protected abstract void PerformBehavior();
}
  