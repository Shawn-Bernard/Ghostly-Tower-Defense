using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth;

    [SerializeField] protected int currentHealth;

    [SerializeField] protected UnityEvent onDeath;
    [SerializeField] protected UnityEvent onDamageTaken;

    private void Awake()
    {
        ResetLife();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage,0);

        onDamageTaken?.Invoke();
    }

    protected void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }

    private void ResetLife()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        onDamageTaken.AddListener(CheckDeath);
    }

    private void OnDisable()
    {
        onDamageTaken.RemoveListener(CheckDeath);
    }
}
