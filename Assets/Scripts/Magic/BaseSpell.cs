// Written by Angel
// Modified by Kevin Chao and Lizbeth

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BaseSpell : MonoBehaviour
{
    [SerializeField] private SpellTemplate spellToCast;
    [SerializeField] private Player player;
    [SerializeField] private SpellEffects spellEffect;
    [SerializeField] private EffectEvent effectCall;

    private SphereCollider spellCollider;
    private Rigidbody spellBody;
    


    // This function serves as a wrapper around the normal Instantiate function
    // It allows the ability to set the player object using the player number when
    // creating the spell. That way a baseSpell created using this wrapper function 
    // will keep track of the player who casted the spell
    /*
    Arguments:
        spellToCreate - The prefab of the spell being casted
        locationOfCreation - The position to spawn the spell at
        rotationOfCreation - A Quaternion providing the rotation of the spell
        playerNumber - The player number of the player who casted the spell
    */
    static public BaseSpell Instantiate(BaseSpell spellToCreate, Vector3 locationOfCreation, Quaternion rotationOfCreation, int playerNumber)
    {
        //Create the spell
        BaseSpell castedSpell = Instantiate(spellToCreate, locationOfCreation, rotationOfCreation);

        // If playerNumber is not null (-1):
        if (playerNumber != -1)
        {
            //Set the player who cast the spell
            castedSpell.player = PlayerManager.Instance.GetPlayer(playerNumber);
        }
        
        if(castedSpell.spellToCast.self == true) //now aoe sticks to the player
        {
            castedSpell.transform.parent = castedSpell.player.transform;
        }

        return castedSpell;
    }


    private void Awake()
    {
        spellCollider = GetComponent<SphereCollider>();
        spellCollider.isTrigger = true;
        spellCollider.radius = spellToCast.radius;

        spellBody = GetComponent<Rigidbody>();
        spellBody.isKinematic = true;

        // Destroy spell after certain time if it does not hit anything
        StartCoroutine(SpellDuration(spellToCast.spellLifetime));

        if (spellToCast.expand == true)
        {
            StartCoroutine(Expansion(.5f));
        }
    }
    
    
    private void Update()
    {
        // Specific to projectile spells
        if (spellToCast.spellSpeed > 0) 
        {
            transform.Translate(Vector3.forward * spellToCast.spellSpeed * Time.deltaTime);
        }
    }


    public SpellTemplate GetSpell()
    {
        return spellToCast;
    }


    private IEnumerator SpellDuration(float time)
    {
        yield return new WaitForSeconds(time);

        effectCall.Invoke(player, null, this);
        Destroy(gameObject);
    }


    private IEnumerator Expansion(float time)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(time);
            this.transform.localScale += new Vector3(1, 0, 1);
        }
    }


    // TODO: Set the player number to the player that was collided with
    private void OnTriggerEnter(Collider collision)
    {
        if (spellToCast.continious == false)
        {
            // Destroy the spell when it collides
            Destroy(gameObject);

            // Apply spell effect at the collision's gameobject
            effectCall.Invoke(player, collision.gameObject, this);
        }
        
        else 
        {
            effectCall.Invoke(player, collision.gameObject, this);
        }
    }


    public void EarlyCast()
    {
        effectCall.Invoke(player, null, this);
        Destroy(gameObject);
    }
}