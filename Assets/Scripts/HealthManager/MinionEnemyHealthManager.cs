
public class MinionEnemyHealthManager : EnemyHealthManager
{
    override protected void NotifyDeath()
    {
        //don't trigger and EnemyDeath event

        //Perhaps later we will add other stuff here as well
    }
}
