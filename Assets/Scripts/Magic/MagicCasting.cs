// Written by Angel
// Modified by Kevin Chao

using UnityEngine;
using UnityEngine.InputSystem;

public class MagicCasting : MonoBehaviour
{
    [SerializeField] private Transform castLocation; // set the location of where the spell is cast from
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

    //Callback for casting a spell
    private void OnCast()
    {
        // If the player is not casting
        if (!casting)
        {
            casting = true;
            timeSinceLastCast = 0.0f;
            CastCurrentSpell();

            Debug.Log($"{selectedElement.GetElementName()} Spell Cast");
        }


    }

    private void Update()
    {
        castButtonDown = playerSpellControls.Gameplay.Cast.triggered && playerSpellControls.Gameplay.Cast.ReadValue<float>() > 0;

        // Hello Angel, I (Lawson) commented out this code when I refactor the control system
        // Since I switch of having a generate C# wrapper script to a PlayerInput component
        // some of the functionality changed. You no longer have access to the cast action (how you called 
        // ReadValue<float>()) through a script. If you want it you will have to extract it from the 
        // PlayerInput component, which requires you to first call getComponent<PlayerInput>(), and then
        // call action("cast") to get the action. Notice that this is not ideal especially since it requires a 
        // string literal. However you should have no need of the action now. The PlayerInput will automatically
        // call OnCast, if defined, as a callback function when the cast action is performed. Notice that I move
        // the code I commented out up above to OnCast. I am leaving it you to delete this commemted out code and 
        // any variables that are no longer necessary. This way you are familar with this change and we can minimize
        // clean up error.
        // will require you  
        // // If the player is not casting and the cast button is pressed
        // if (!casting && castButtonDown)
        // {
        //     casting = true;
        //     timeSinceLastCast = 0.0f;
        //     CastCurrentSpell();

        //     Debug.Log($"{selectedElement.GetElementName()} Spell Cast");
        // }

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
        Instantiate(spellToCast, castLocation.position, castLocation.rotation); // create spell at castlocation
    }
}