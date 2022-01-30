using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 5;
    [SerializeField] private int maxHealth = 10;
    protected int currentHealth;

    public delegate void SetPlayerHealth(int newHealth);
    public delegate void SetPlayerMaxHealth(int maxHealth);

    public SetPlayerHealth setPlayerHealthMethod;
    public SetPlayerMaxHealth setPlayerMaxHealthMethod;


    // Start() initialize the current object's health according to startingHealth.
    // Having a startingHealth is necessary in case a player gets revived.
    private void Start()
    {
        currentHealth = startingHealth;
        string nameTag = gameObject.tag;
        Debug.Log("Current " + nameTag + " Health: " + currentHealth.ToString());

        InitializeHealthBar();
    }


    private void InitializeHealthBar()
    {
        setPlayerMaxHealthMethod(maxHealth);
        setPlayerHealthMethod(currentHealth);
    }


    public void GainHealth(int numHeal)
    {
        currentHealth += numHeal;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        setPlayerHealthMethod(currentHealth);
    }


    // LoseHealth() substracts object's health according to the number of damage.
    public virtual void LoseHealth(int numDamage)
    {
        currentHealth -= numDamage;

        string nameTag = gameObject.tag;
        Debug.Log("Current " + nameTag + " Health: " + currentHealth.ToString());
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            setPlayerHealthMethod(currentHealth);

            Destroy(gameObject);

            //This is a temp line to prevent error
            //Liz feel free to move/change this line
            //however is necessary
            //EventManager.Instance.Notify(Event.EventTypes.PlayerDeath);
            EventManager.Instance.Notify(Event.EventTypes.EnemyDeath);
        }

        else
            setPlayerHealthMethod(currentHealth);
    }
}