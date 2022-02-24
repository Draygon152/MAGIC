// Written by Lawson McCoy
// Modified by Kevin Chao

using UnityEngine;
using TMPro;

public class SetEnemyTypeLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text label;  // The text object that displays the enemy's type
    [SerializeField] private string typeName; // The name of the type for this enemy,
                                              // will probably be change to be stored in a ScriptableObject


    private void Start()
    {
        label.text = typeName;
    }
}