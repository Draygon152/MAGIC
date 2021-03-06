// Written by Angel
// Modified by Kevin Chao, Lawson, and Lizbeth

using UnityEngine;
using UnityEngine.InputSystem;

public class MagicCasting : MonoBehaviour
{
    [SerializeField] private Transform castLocation; // Location where the spell is cast from
    [SerializeField]private BaseSpell spellToCast;
    private Element selectedElement;

    private bool casting = false; // Default state of casting magic is false
    private float castCooldown;   // Time between spellcasts
    private float timeSinceLastCast;
    private int playerNumber; // Stores player number so it can be referenced when casting a spell

    private BaseSpell instantiatedSpell;



    private void Awake()
    {

        if (this.gameObject.tag != "Player")
        {
            // Set playerNumber as null (-1)
            playerNumber = -1;
        }

        else
        {
            // Set playerNumber
            playerNumber = this.gameObject.GetComponent<PlayerInput>().playerIndex;
        }
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
        float range;

        if (spellToCast.GetSpell().spellSpeed != 0)
        {
            range = spellToCast.GetSpell().spellSpeed * spellToCast.GetSpell().spellLifetime;
        }

        else
        {
            range = -1.0f; // denoting a stationary spell;
        }

        return range;
    }


    private void ChangeTransform()
    {
        if (!casting)
        {
            if (spellToCast.GetSpell().spellSpeed == 0.0f)
            {
                castLocation.localPosition = new Vector3(0, -1, 0);
            }

            else
            {
                if (spellToCast.GetSpell().spellSpeed < 0.5f)
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
        instantiatedSpell = BaseSpell.Instantiate(spellToCast, castLocation.position, castLocation.rotation, playerNumber);
    }


    public float GetTimeSinceLastCast()
    {
        return timeSinceLastCast;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject.tag == "Player" && collision.GetComponent<Collider>().tag == "pickups")
        {
            spellToCast = collision.GetComponent<SpellItem>().GetSpell();
            castCooldown = spellToCast.GetSpell().timeBetweenCasts;

            SelectedSpellUI playerSpellUI = this.gameObject.GetComponentInChildren<SelectedSpellUI>();
            playerSpellUI.InitializeSpellUI(this);
            playerSpellUI.ChangeSpellMaxCooldown(castCooldown);
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


    public void EnemyCast()
    {
        if (!casting)
        {
            casting = true;
            CastCurrentSpell();
        }
    }


    private void OnActivate()
    {
        if (instantiatedSpell != null)
        {
            instantiatedSpell.EarlyCast();
        }
    }


    // A simple function to allow an AI to cast spells
    // There might be a better way to do this, but for I 
    // am going with this becasue of time constraint
    public void AIOnCast()
    {
        // pass on the function call to OnCast
        // if any preprocessing needs to be done
        // for an AI to cast a spell the it should
        // be done here

        OnCast();
    }
}