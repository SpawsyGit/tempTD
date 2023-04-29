public interface IEntityLife
{
    public int totalLife { get; }
    public void OnTakeDamage();
    public void OnDie();
}