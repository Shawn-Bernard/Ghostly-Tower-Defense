using UnityEngine;

public class PlayerBaseHealthSystem : HealthSystem
{
    [SerializeField] private StringEvent healthStringEvent;

    private void Start()
    {
        UpdateHealthText();
    }
    private void OnEnable()
    {
        onDamageTaken.AddListener(UpdateHealthText);
        onDamageTaken.AddListener(CheckDeath);
    }

    private void OnDisable()
    {
        onDamageTaken.RemoveListener(UpdateHealthText);
        onDamageTaken.RemoveListener(CheckDeath);
    }

    private void UpdateHealthText()
    {
        healthStringEvent.RaiseEvent(currentHealth.ToString());
    }
}
