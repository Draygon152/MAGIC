// Written by Angel
// Modified by Kevin Chao and Lawson

using UnityEngine;
using UnityEngine.InputSystem;

public class MagicCasting : MonoBehaviour
{
    [SerializeField] private Transform castLocation; // Location where the spell is cast from

    private BaseSpell spellToCast;
    private Element selectedElement;

    private bool casting = false; // Default state of casting magic is false
    private float castCooldown;   // Default time between spellcasts. Need to replace with individual spell casting time, placeholder
    private float timeSinceLastCast;
    private int playerNumber; // Stores player number so it can be referenced when casting a spell

    private BaseSpell shellofSpell;



    private void Awake()
    {
        // Set playerNumber
        playerNumber = this.gameObject.GetComponent<PlayerInput>().playerIndex;
    }


    private void Update()
    {
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


    public float GetSpellRange()
    {
        return spellToCast.GetSpell().spellSpeed * spellToCast.GetSpell().spellLifetime;
    }

    private void ChangeTransform()
    {
        float cooldownRemaining = HUD.Instance.ReturnCooldown(playerNumber);

        if (cooldownRemaining == 0)
        {
            if (spellToCast.GetSpell().spellSpeed == 0)
            {
                castLocation.localPosition = new Vector3(0, -0.5f, 0);
            }

            else
            {
                if (spellToCast.GetSpell().spellSpeed < .5)
                {
                    castLocation.localPosition = new Vector3(0, 0, 1);
                }
                else
                {
                    castLocation.localPosition = new Vector3(0, 0, 0);
                }
            }
        }
    }


    private void CastCurrentSpell()
    {
        // Create spell at castLocation
        shellofSpell = BaseSpell.Instantiate(spellToCast, castLocation.position, castLocation.rotation, playerNumber);
    }


    public float GetTimeSinceLastCast()
    {
        return timeSinceLastCast;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == "pickups")
        {
            spellToCast = collision.GetComponent<SpellItem>().GetSpell();
            castCooldown = spellToCast.GetSpell().timeBetweenCasts;
            
            HUD.Instance.SetPlayerSpellCaster(playerNumber, this);
            HUD.Instance.SetPlayerMaxCooldown(playerNumber, spellToCast.GetSpell().timeBetweenCasts);
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

    private void OnActivate()
    {
        if (shellofSpell != null)
        {
            shellofSpell.EarlyCast();
        }
    }

    //A simple function to allow an AI to cast the spell
    //There might be a better way to do this, but for I 
    //am going with this becasue of time constraint
    public void AIOnCast()
    {
        //pass on the function call to OnCast
        //if any preprocessing needs to be done
        //for an AI to cast a spell the it should
        //be done here
        OnCast();
    }
}