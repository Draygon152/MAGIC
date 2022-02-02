//Worked on by Angel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======
    //Effects is only for the base level spells atm

    private GameObject playerinfo; //the player
    private GameObject entity; //the enemy/object that the spell collided with
    [SerializeField] private SpellGivesDamage ouch; 
    private BaseSpell currentSpell; //spell being used

    public void Base_Effects(Element.ElementTypes currentElement, GameObject player, GameObject locationEntity, BaseSpell passedSpell)
    {
        playerinfo = player;
        entity = locationEntity;
        currentSpell = passedSpell;
        switch (currentElement) //check to see what element corresponds with the element of the spell, then cast the corresponding effect
        {
            case Element.ElementTypes.Arcane:
                arcaneEffect();
                break;
            case Element.ElementTypes.Wind:
                windEffect();
                break;
            case Element.ElementTypes.Fire:
                fireEffect();
                break;
            case Element.ElementTypes.Nature:
                arcaneEffect();
                break;
            case Element.ElementTypes.Ice:
                iceEffect();
                break;
            case Element.ElementTypes.Lightning:
                lightningEffect();
                break;
            default:
                throw new Exception("Element not found");
        }
    }

    void lightningEffect()
    {
        playerinfo.transform.position = entity.transform.position; //Teleports ontop of enemy, takes damage as a result
    }

    void arcaneEffect()
    {
        print("TESTING1");
    }

    void fireEffect()
    {
        print("tick");
    }

    void windEffect()
    {
        Vector3 direction = entity.transform.position - playerinfo.transform.position;
        entity.GetComponent<Rigidbody>().AddForce(direction * 100);
        //pushes the enemy away from the point of collision
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
