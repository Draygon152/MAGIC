// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

// Template for spell creation, added as option in Asset Menu
[CreateAssetMenu(fileName = "New Spell", menuName = "Magic Spells")]
public class SpellTemplate : ScriptableObject
{
    public int damage;       // How much damage the spell will deal
    public int numInstances; // How many instances of this spell should be available at once (-1 if unlimited)

    public float timeBetweenCasts; // How long it should take before user can cast the spell again
    public float castSpeed;        // How long spell should take to cast
    public float spellSpeed;       // The speed the spell travels at
    public float spellLifetime;    // How long the spell will exist before fading out of existance (if it does not hit anything)
    public float effectDuration;
    public float radius; // Radius of spell effect
    public bool continuous;

    // Determines whether spell should be cast centered on self or not
    public bool self;
    public bool expand;

    public ElementTypes.Elements element;
}