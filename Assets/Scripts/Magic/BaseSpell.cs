// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BaseSpell : MonoBehaviour
{
    public SpellTemplate spellToCast;
    private SphereCollider spellCollider;
    private Rigidbody spellBody;
    public Effects spellEffect;
    public GameObject player;



    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spellCollider = GetComponent<SphereCollider>();
        spellCollider.isTrigger = true;
        spellCollider.radius = spellToCast.radius;

        spellBody = GetComponent<Rigidbody>();
        spellBody.isKinematic = true;

        Destroy(gameObject, spellToCast.spellLifetime); // Destroy spell after certain time if it does not hit anything
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
        spellEffect.Base_Effects(spellToCast.element, player, collision.gameObject, this);
    }
}

//MAKE THE LIGHTNING AFFECT APPLICABLE EVEN IF IT DOES NOT COLLIDE