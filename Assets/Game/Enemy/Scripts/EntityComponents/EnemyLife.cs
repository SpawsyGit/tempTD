using UnityEngine;

public class EnemyLife : MonoBehaviour, IEntityLife
{
    private EntityLifeManager lifeManager;
    private SpriteBlinker spriteBlinker;

    [SerializeField] private SpriteRenderer[] spritesToBlink;

    [InspectorName("Total Life")]
    [SerializeField] private int _totalLife;
    [SerializeField] private float blinkLength;
    [SerializeField] private float blinkFrequency;

    public int totalLife => _totalLife;
    public int currentLife => lifeManager.currentLife;

    
    private void Start()
    {
        lifeManager = new EntityLifeManager(this);
        spriteBlinker = new SpriteBlinker(spritesToBlink, this);
    }

    public void AddDamage(int amount) => lifeManager.AddDamage(amount);
    public void RecoverLife(int amount) => lifeManager.RecoverLife(amount);

    public void OnTakeDamage()
    {
        if (spriteBlinker.isBlinking) spriteBlinker.StopBlink();
        spriteBlinker.StartBlinkWith(blinkLength, blinkFrequency);
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
