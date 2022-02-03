// Written by Angel

using UnityEngine;

// Allows us to make different spells using this template
[CreateAssetMenu(fileName = "New Spell", menuName = "Magic Spells")]
public class SpellTemplate : ScriptableObject
{
    public int damage; // how much damage the spell will deal
    public float timeBetweenCasts; // how long it should take before user can cast the spell again
    public float numInstances;     // how many instances of this spell should be avaliable at once (-1 if unlimited)
    public float castSpeed;        // how long it should take to cast
    public float spellSpeed;       // the speed the spell travels at
    public float spellLifetime;    // how long the spell will exist before fading out of existance (if it does not hit anything)
    public float radius;
    public float self; // 1 should be on self, 0 will be aoe (check speed, if speed is 0)
    public Element.ElementTypes element;
}
