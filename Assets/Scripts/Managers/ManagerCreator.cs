using System.Collections.Generic;
using UnityEngine;

public class ManagerCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] managerPrefabs;
    private static List<GameObject> managerRefs = new List<GameObject>();



    private void Start()
    {
        if (managerRefs.Count == 0)
        {
            foreach (GameObject manager in managerPrefabs)
            {
                managerRefs.Add(Instantiate(manager));
            }
        }
    }
}