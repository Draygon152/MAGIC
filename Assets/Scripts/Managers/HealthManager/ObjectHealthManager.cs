// Written by Lizbeth
// Modified by Kevin Chao and Angel

using UnityEngine;

// TODO: Extract code for spawning spell item drop to a "lootable object" class
public class ObjectHealthManager : HealthManager
{
    [SerializeField] private SpellItem spellItem;



    protected override void Start()
    {
        base.Start();
        EventManager.Instance.Subscribe(EventTypes.Events.ResetGame, RespawnCrate);
    }


    public override void LoseHealth(int damageAmount)
    {
        currentHealth -= damageAmount;

        // If health becomes 0 or less, object destroyed
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SpawnSpellItem();
        }
    }


    private void SpawnSpellItem()
    {
        if (gameObject.tag == "Crate")
        {
            Instantiate(spellItem, gameObject.transform.position, gameObject.transform.rotation);
        }
    }


    public void RespawnCrate()
    {
        gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe(EventTypes.Events.ResetGame, RespawnCrate);
    }
}