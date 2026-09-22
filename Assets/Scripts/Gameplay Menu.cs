using UnityEngine;
using TMPro;
public class GameplayMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baseHealth;
    [SerializeField] private StringEvent healthStringEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetBaseHealthText(string health)
    {
        baseHealth.text = $"Base Health : {health}";
    }

    private void OnEnable()
    {
        healthStringEvent.gameEvent += SetBaseHealthText;
    }
    private void OnDisable()
    {
        healthStringEvent.gameEvent -= SetBaseHealthText;
    }
}
