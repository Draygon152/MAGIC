using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HealthPotionScript : MonoBehaviour
{
    private BoxCollider pickUpCollider;
    private Rigidbody pickUpBody;
    // Start is called before the first frame update
    void Start()
    {
        pickUpCollider = GetComponent<BoxCollider>();
        pickUpCollider.isTrigger = true;

        pickUpBody = GetComponent<Rigidbody>();
        pickUpBody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // If a player steps onto the SpellItem, it should disappear
        if (collision.GetComponent<Collider>().tag == "Player")
        {
            collision.GetComponent<PlayerHealthManager>().GainHealth(25);
            print("health gain");
            Destroy(gameObject);
        }
    }
}
