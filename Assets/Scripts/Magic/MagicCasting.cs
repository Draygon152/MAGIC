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
    private float castCooldown;   // Default time between spellcasts. Need to replace with individual spell casting time, placeholder
    private float timeSinceLastCast;
    private int playerNumber; // Stores player number so it can be referenced when casting a spell



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
            castCooldown = spellToCast.GetSpell().timeBetweenCasts;
            if (playerNumber == PlayerManager.PLAYER_1)
            {
                HUD.Instance.SetP1SpellCaster(this);
                HUD.Instance.SetP1MaxCooldown(spellToCast.GetSpell().timeBetweenCasts);
            }

            else if (playerNumber == PlayerManager.PLAYER_2)
            {
                HUD.Instance.SetP2SpellCaster(this);
                HUD.Instance.SetP2MaxCooldown(spellToCast.GetSpell().timeBetweenCasts);
             }
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
        if(!casting)
        {
            casting = true;
            CastCurrentSpell();
        }
    }
}