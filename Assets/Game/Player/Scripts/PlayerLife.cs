using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour, IEntityLife
{
    private EntityLifeManager lifeManager;

    [InspectorName("Total Life")]
    [SerializeField] private int _totalLife = 5;
    public int totalLife => _totalLife;

    [SerializeField] private Image lifebarFill;
    [SerializeField] private GameObject gameOverScreen;

    private void Start()
    {
        Time.timeScale = 1;
        lifeManager = new EntityLifeManager(this);
    }

    public void AddDamage(int amount) => lifeManager.AddDamage(amount);
    public void RecoverLife(int amount) => lifeManager.RecoverLife(amount);

    public void OnDie()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        UpdateLifeBar();
    }

    public void OnTakeDamage()
    {
        UpdateLifeBar();
    }

    private void UpdateLifeBar()
    {
        lifebarFill.fillAmount = (float)lifeManager.currentLife / (float)totalLife;
    }

}