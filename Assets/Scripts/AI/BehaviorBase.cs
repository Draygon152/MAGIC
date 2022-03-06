using UnityEngine;

abstract public class BehaviorBase : MonoBehaviour
{
    //The behavior of the AI
    abstract protected void PerformBehavior();

    //Performs this behavior every frame
    private void FixedUpdate()
    {
        PerformBehavior();
    }
}
  