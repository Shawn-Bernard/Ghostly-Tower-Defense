using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;

    [SerializeField] private int currentHealth;

    [SerializeField] private UnityEvent onDeath;

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
        onDeath?.Invoke();
    }

    private void ResetLife()
    {
        currentHealth = maxHealth;
    }
}
