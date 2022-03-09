// Written by Lawson

using UnityEngine;

public abstract class BehaviorBase : MonoBehaviour
{
    //Performs this behavior every frame
    private void FixedUpdate()
    {
        PerformBehavior();
    }


    //The behavior of the AI
    protected abstract void PerformBehavior();
}
  