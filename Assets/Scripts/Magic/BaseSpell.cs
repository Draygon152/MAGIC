//Worked on by Angel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BaseSpell : MonoBehaviour
{
    public SpellTemplate SpellToCast;
    private SphereCollider spellCollider;
    private Rigidbody spellBody;
    public Effects spellEffect;
    public GameObject player;
    public GameObject hitLocation;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spellCollider = GetComponent<SphereCollider>();
        spellCollider.isTrigger = true;
        spellCollider.radius = SpellToCast.radius;

        spellBody = GetComponent<Rigidbody>();
        spellBody.isKinematic = true;

        Destroy(gameObject, SpellToCast.spellLifetime); //destroy spell after certain time if it does not hit anything
    }

    private void Update()
    {
        if (SpellToCast.spellSpeed > 0) //Projectile spell
        {
            transform.Translate(Vector3.forward * SpellToCast.spellSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        hitLocation = collision.gameObject;
        Destroy(gameObject); //destroy the spell when it collides
        spellEffect.Base_Effects(SpellToCast.element, player, hitLocation, this);
    }
}

//MAKE THE LIGHTNING AFFECT APPLICABLE EVEN IF IT DOES NOT COLLIDE