public class EntityLifeManager
{
    private IEntityLife entityLife;

    public int currentLife { get; private set; }

    public EntityLifeManager(IEntityLife entityLife)
    {
        this.entityLife = entityLife;
        currentLife = entityLife.totalLife;
    }

    public void AddDamage(int amount)
    {
        currentLife -= amount;
        if (currentLife <= 0) entityLife.OnDie();
        else entityLife.OnTakeDamage();
    }

    public void RecoverLife(int amount)
    {
        currentLife += amount;
        if (currentLife > entityLife.totalLife) currentLife = entityLife.totalLife;
    }
}