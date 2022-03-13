// Written by Angel
// Modified by Kevin Chao

using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    [SerializeField] private SpellDamageGiver spellDamageGiver;



    public void TeleportationEffect(Player player, GameObject target, BaseSpell spell)
    {
        player.transform.position = spell.transform.position;
    }


    public void PushbackEffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<Rigidbody>() != null)
            {
                Vector3 direction = target.transform.position - player.transform.position;

                if(direction[0] > 1 || direction[0] < -1)
                {
                    direction[0] *= 5;
                }

                if(direction[2] > 1 || direction[2] < -1)
                {
                    direction[2] *= 5;
                }

                target.GetComponent<Rigidbody>().AddForce(direction * 250);
            }
        }

        catch
        {
        }
    }


    public void HealEffect(Player player, GameObject target, BaseSpell spell)
    {
        player.GetComponent<PlayerHealthManager>().GainHealth(spell.GetSpell().damage);
    }


    public void StunEffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null && target.GetComponent<HealthManager>().GetHealth() > 0 && target.GetComponent<DebuffManager>() != null)
            {
                target.GetComponent<DebuffManager>().DamageChange(spell.GetSpell().effectDuration);
                target.GetComponent<DebuffManager>().SpeedChange(spell.GetSpell().effectDuration, 0f);
            }
        }

        catch
        { 
        }
    }


    public void SustainedDamageEffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<DebuffManager>() != null)
            {
                target.GetComponent<DebuffManager>().SustainedDamage(spell.GetSpell().effectDuration, spell.GetSpell().damage);
            }
        }

        catch
        {
        }
    }


    public void SlowEffect(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            if (target != null || target.GetComponent<DebuffManager>() != null)
            {
                target.GetComponent<DebuffManager>().SpeedChange(spell.GetSpell().effectDuration, 0.5f);
            }
        }

        catch
        {
        }
    }


    public void BounceDamage(Player player, GameObject target, BaseSpell spell)
    {
        try
        {
            GameObject currenttarget = target;
            while (currenttarget != null)
            {
                currenttarget = ReturnEnemyinRange(3f, currenttarget);
                if(currenttarget != null)
                {
                    spellDamageGiver.UseDamage(currenttarget, spell.GetSpell().damage);
                }
            }
        }

        catch
        {
        }
    }
    

    // TODO: Finish implementation
    private GameObject ReturnEnemyinRange(float range, GameObject currenttarget)
    {
        return null;
    }
}