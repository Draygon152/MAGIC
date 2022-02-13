// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    [SerializeField] private Transform castlocation; // set the location of where the spell is cast from
    [SerializeField] private ElementList listOfSpells;

    private BaseSpell spellToCast;
    private Element selectedElement;
    private PlayerControls playerSpellControls;

    private bool casting = false;    // the default state of casting magic is false
    private float castCooldown;      // default time between spellcasts. need to replace with individual spell's casting time also placeholder
    private float timeSinceLastCast;
    private bool castButtonDown;



    private void Awake()
    {
        // Initializes the player controls
        playerSpellControls = new PlayerControls();
    }


    private void OnEnable()
    {
        // Enables control for the player's spell casting
        playerSpellControls.Enable();
    }


    private void OnDisable()
    {
        // Disables control for the player's spell casting
        playerSpellControls.Disable();
    }


    private void Update()
    {
        castButtonDown = playerSpellControls.Spells.Cast.triggered && playerSpellControls.Spells.Cast.ReadValue<float>() > 0;

        // If the player is not casting and the cast button is pressed
        if (!casting && castButtonDown)
        {
            casting = true;
            timeSinceLastCast = 0.0f;
            CastCurrentSpell();

            Debug.Log($"{selectedElement.GetElementName()} Spell Cast");
        }

        if (casting)
        {
            timeSinceLastCast += Time.deltaTime;  // Increase time since last cast by time that passed

            if (timeSinceLastCast > castCooldown) // If cooldown expired, next cast available
                casting = false;
        }
    }

    public void InitializeSpell(Element elem)
    {
        selectedElement = elem;
        spellToCast = listOfSpells.GetSpell(selectedElement.GetElementType());
        castCooldown = spellToCast.SpellToCast.timeBetweenCasts;
    }


    public BaseSpell ReturnSpell()
    {
        return spellToCast;
    }


    private void CastCurrentSpell()
    {
        Instantiate(spellToCast, castlocation.position, castlocation.rotation); // create spell at castlocation
    }
}