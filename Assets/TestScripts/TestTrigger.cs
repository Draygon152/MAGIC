using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public GameObject victoryScreenUI;

    public void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT!");
        victoryScreenUI.SetActive(true);
    }
}
