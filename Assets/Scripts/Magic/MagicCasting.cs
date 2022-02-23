// Written by Angel
// Modified by Kevin Chao and Lawson

using UnityEngine;
using UnityEngine.InputSystem;

public class MagicCasting : MonoBehaviour
{
    [SerializeField] private Transform castLocation; // Location where the spell is cast from

    private BaseSpell spellToCast;
    private Element selectedElement;
    private PlayerControls playerSpellControls;

    private bool casting = false; // Default state of casting magic is false
    private float castCooldown;   // Default time between spellcasts. Need to replace with individual spell casting time, placeholder
    private float timeSinceLastCast;
    private bool castButtonDown;
    private int playerNumber; // Stores player number so it can be referenced when casting a spell



    private void Awake()
    {
        // Initialize player controls
        playerSpellControls = new PlayerControls();

        // Set playerNumber
        playerNumber = this.gameObject.GetComponent<PlayerInput>().playerIndex;
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

        // castButtonDown = playerSpellControls.Gameplay.Cast.triggered && playerSpellControls.Gameplay.Cast.ReadValue<float>() > 0;

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
            {
                casting = false;
                timeSinceLastCast = 0.0f;
            }
        }
    }


    public void InitializeSpell(Element elem)
    {
        selectedElement = elem;
        spellToCast = ElementList.Instance.GetSpell(selectedElement.GetElementType());
        castCooldown = spellToCast.GetSpell().timeBetweenCasts;
    }


    public SpellTemplate GetSpell()
    {
        return spellToCast.GetSpell();
    }


    private void ChangeTransform()
    {
        if (spellToCast.GetSpell().spellSpeed == 0)
        {
            castLocation.localPosition = new Vector3(0, -0.5f, 0);
        }

        else
        {
            castLocation.localPosition = new Vector3(0, 0, 0);
        }
    }


    private void CastCurrentSpell()
    {
        // Create spell at castLocation
        BaseSpell.Instantiate(spellToCast, castLocation.position, castLocation.rotation, playerNumber);
    }


    public float GetTimeSinceLastCast()
    {
        return timeSinceLastCast;
    }


    // TODO: Add code that will update the correct player's HUD text with new spell name
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == "pickups")
        {
            Debug.Log($"Picking up SpellItem {collision.name}");

            spellToCast = collision.GetComponent<SpellItem>().GetSpell();

            if (playerNumber == PlayerManager.PLAYER_1)
                HUD.Instance.SetP1SpellCaster(this);

            else if (playerNumber == PlayerManager.PLAYER_2)
                HUD.Instance.SetP2SpellCaster(this);
        }
    }


    // TODO: Finish transition to new casting system
    // Callback for casting a spell
    private void OnCast()
    {
        ChangeTransform();

        // If the player is not casting
        if (!casting)
        {
            casting = true;
            CastCurrentSpell();
        }
    }
}