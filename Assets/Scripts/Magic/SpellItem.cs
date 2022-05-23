// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using TMPro;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SpellItem : MonoBehaviour
{
    private SphereCollider pickUpCollider;
    private Rigidbody pickUpBody;
    private BaseSpell containedSpell;

    [SerializeField] TextMeshProUGUI m_Object;



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
        // containedSpell = SpellList.Instance.GetRandomSpell();
        containedSpell = SpellList.Instance.GetTestSpell();
        m_Object.text = containedSpell.GetSpell().name;
    }


    public BaseSpell GetSpell()
    {
        return containedSpell;
    }


    private void OnTriggerEnter(Collider collision)
    {
        // If a player steps onto the SpellItem, it should disappear
        if (collision.GetComponent<Collider>().tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}