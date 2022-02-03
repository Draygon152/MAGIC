using UnityEngine;

//Written by Liz
public class ObjectGivesDamage : DamageGiverManager
{
    [SerializeField] private int damageDealt;

    // This function checks if player collided to Object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided!");
            PlayerHealthManager player = collision.gameObject.GetComponentInParent<PlayerHealthManager>();
            DamagePlayer(player, damageDealt);
        }
    }
}