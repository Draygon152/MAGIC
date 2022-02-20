// Written by Angel

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SpellItem : MonoBehaviour
{
    private SphereCollider pickUpCollider;
    private Rigidbody pickUpBody;
    private BaseSpell containedSpell;
    [SerializeField] private SpellList spellList;



    private void Awake()
    {
        pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.isTrigger = true;
        pickUpCollider.radius = .25f;

        pickUpBody = GetComponent<Rigidbody>();
        pickUpBody.isKinematic = true;
    }


    private void Start()
    {
        containedSpell = spellList.spellRandomizer();
        Debug.Log($"SPELL IN ITEM: {containedSpell}");
    }


    public BaseSpell returnContainedSpell()
    {
        return containedSpell;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}