using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth;

    [SerializeField] protected int currentHealth;

    [SerializeField] protected UnityEvent onDeath;
    [SerializeField] protected UnityEvent onDamageTaken;

    [SerializeField] private AudioClip[] damagedSounds;
    [SerializeField] private AudioClip[] deathSounds;

    private void Awake()
    {
        ResetLife();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage,0);

        onDamageTaken?.Invoke();
        if (damagedSounds != null) SoundManager.Instance.PlayRandomSounds(damagedSounds);
        CheckDeath();
    }

    protected void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            if (deathSounds != null) SoundManager.Instance.PlayRandomSounds(deathSounds);
            onDeath?.Invoke();
        }
    }

    private void ResetLife()
    {
        currentHealth = maxHealth;
    }
}
