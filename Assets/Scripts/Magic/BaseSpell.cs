// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BaseSpell : MonoBehaviour
{
    public SpellTemplate SpellToCast;
    private SphereCollider spellCollider;
    private Rigidbody spellBody;
    public Effects spellEffect;
    public GameObject player;
    [SerializeField] private spellEffectCall spellCall;



    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spellCollider = GetComponent<SphereCollider>();
        spellCollider.isTrigger = true;
        spellCollider.radius = SpellToCast.radius;

        spellBody = GetComponent<Rigidbody>();
        spellBody.isKinematic = true;
        StartCoroutine(spellDuration(SpellToCast.spellLifetime)); // Destroy spell after certain time if it does not hit anything
    }

    IEnumerator spellDuration(float time)
    {
        yield return new WaitForSeconds(time);
        //spellCall.Invoke(player, null, this);
        spellEffect.time_Effects(SpellToCast.element, player, this);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (SpellToCast.spellSpeed > 0) // Projectile spell
        {
            transform.Translate(Vector3.forward * SpellToCast.spellSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        //spellCall.Invoke(player, collision.gameObject, this);
        Destroy(gameObject); // Destroy the spell when it collides
        // Apply spell effect at the collision's gameobject
        spellEffect.hit_Effects(SpellToCast.element, player, collision.gameObject, this);
    }
}

//MAKE THE LIGHTNING AFFECT APPLICABLE EVEN IF IT DOES NOT COLLIDE