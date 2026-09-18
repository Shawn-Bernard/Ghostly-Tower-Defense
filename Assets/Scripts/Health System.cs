using UnityEngine;
using UnityEngine.Rendering;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    private void Awake()
    {
        ResetLife();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage,0);
        //Debug.Log($"Current Health : {currentHealth}");

        if (currentHealth <= 0 )
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void ResetLife()
    {
        currentHealth = maxHealth;
    }
}
