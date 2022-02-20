// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BaseSpell : MonoBehaviour
{
    public SpellTemplate spellToCast;
    private SphereCollider spellCollider;
    private Rigidbody spellBody;
    public Effects spellEffect;
    public GameObject player;

    //This function serves as a wrapper around the normal Instantiate function
    //It allows the ability to set the player object using the player number when
    //creating the spell. That way a baseSpell created using this wrapper function 
    //will keep track of the player who casted the spell
    /*
    Arguements:
        spellToCreate - The prefab of the spell being casted
        locationOfCreation - The position to spawn the spell at
        rotationOfCreation - A Quaternion providing the rotation of the spell
        playerNumber - The player number of the player who casted the spell
    */
    static public void Instantiate(BaseSpell spellToCreate, Vector3 locationOfCreation, Quaternion rotationOfCreation, int playerNumber)
    {
        //Create the spell
        BaseSpell castedSpell = Object.Instantiate(spellToCreate, locationOfCreation, rotationOfCreation);

        //Set the player who cast the spell
        castedSpell.player = PlayerManager.Instance.GetPlayer(playerNumber).gameObject;
    }


    private void Awake()
    {
        spellCollider = GetComponent<SphereCollider>();
        spellCollider.isTrigger = true;
        spellCollider.radius = spellToCast.radius;

        spellBody = GetComponent<Rigidbody>();
        spellBody.isKinematic = true;

        StartCoroutine(spellDuration(spellToCast.spellLifetime)); // Destroy spell after certain time if it does not hit anything
    }

    IEnumerator spellDuration(float time)
    {
        yield return new WaitForSeconds(time);
        //spellCall.Invoke(player, null, this);
        spellEffect.Base_Effects(spellToCast.element, player, null, this);
        Destroy(gameObject);
    }
    
    

    private void Update()
    {
        if (spellToCast.spellSpeed > 0) // Projectile spell
        {
            transform.Translate(Vector3.forward * spellToCast.spellSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject); // Destroy the spell when it collides
        // Apply spell effect at the collision's gameobject
        print(spellToCast.element);
        spellEffect.Base_Effects(spellToCast.element, player, collision.gameObject, this);
    }

    //Set the player number to the player who 
}

//MAKE THE LIGHTNING AFFECT APPLICABLE EVEN IF IT DOES NOT COLLIDE