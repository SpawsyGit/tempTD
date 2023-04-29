using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    
    public void Attack()
    {
        PlayerLife playerLife = FindObjectOfType<PlayerLife>();
        playerLife.AddDamage(damage);
        GetComponent<EnemyLife>().OnDie();
    }
}