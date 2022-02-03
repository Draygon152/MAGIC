// Written by Angel

using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    [SerializeField] private Transform castlocation; // set the location of where the spell is cast from
    private BaseSpell spellToCast;
    [SerializeField] private ElementList listOfSpells;
    private Element SelectedElement;
    private PlayerControls PlayerControlsspells;
    private bool casting = false; // the default state of casting magic is false
    private float castingtime; // = 1f;   // default time between casts. need to replace with individual spell's casting time also placeholder
    private float current_cast_time; // container for the time it has been since last cast



    private void Awake()
    {
        // Initializes the player controls
        PlayerControlsspells = new PlayerControls();
    }


    private void OnEnable()
    {
        // Enables control for the player's spell casting
        PlayerControlsspells.Enable();
    }


    private void OnDisable()
    {
        // Disables control for the player's spell casting
        PlayerControlsspells.Disable();
    }


    private void Update()
    {
        castingtime = spellToCast.GetComponent<BaseSpell>().SpellToCast.timeBetweenCasts;
        bool cast_button_down = PlayerControlsspells.Spells.Cast.triggered && PlayerControlsspells.Spells.Cast.ReadValue<float>() > 0;

        // if the player is not casting and the cast button is pressed
        if (!casting && cast_button_down)     
        {
            casting = true; // toggle casting to true
            current_cast_time = 0;  // set the time since last cast down to 0;
            CastCurrentSpell();
            print("ABRA CADABRA!"); // test to see if it works
        }

        if (casting) // If casting is true
        {
            current_cast_time += Time.deltaTime; // add time from the last time it was cast
            if (current_cast_time > castingtime) // check to see if enough time has passed between castings
            {
                casting = false;
            }
        }
    }


    public void SetElement(Element SE) // SE = Selected Element
    {
        SelectedElement = SE;
        spellToCast = listOfSpells.Return_Spell(SelectedElement.GetElementType());
    }


    public BaseSpell returnSpell()
    {
        return spellToCast;
    }


    private void CastCurrentSpell()
    {
        Instantiate(spellToCast, castlocation.position, castlocation.rotation); // create spell at castlocation
    }
}