using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Written by Lawson McCoy
public class SetEnemyTypeLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text label; //The text object that displays the enemy's type
    [SerializeField] private string typeName; //The name of the type for this enemy,
                                              //will probably be change to be stored in a SO

    void Start()
    {
        label.text = typeName;
    }
}
