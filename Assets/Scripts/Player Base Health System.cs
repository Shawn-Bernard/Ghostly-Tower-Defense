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
    }

    private void OnDisable()
    {
        onDamageTaken.RemoveListener(UpdateHealthText);
    }

    private void UpdateHealthText()
    {
        healthStringEvent.RaiseEvent(currentHealth.ToString());
    }
}
