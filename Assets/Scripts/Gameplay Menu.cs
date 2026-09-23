using UnityEngine;
using TMPro;
public class GameplayMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baseHealth;
    [SerializeField] private StringEvent healthStringEvent;

    [SerializeField] private TextMeshProUGUI waveInfo;
    [SerializeField] private StringEvent waveStringEvent;

    [SerializeField] private TextMeshProUGUI roundInfo;
    [SerializeField] private StringEvent roundStringEvent;

    private void SetBaseHealthText(string health)
    {
        baseHealth.text = $"Base Health : {health}";
    }

    private void SetWaveText(string currentWave,string totalWaves)
    {
        waveInfo.text = $"Wave {currentWave}/{totalWaves}";
    }

    private void SetRoundText(string currentRound, string totalRounds)
    {
        roundInfo.text = $"Round {currentRound}/{totalRounds}";
    }

    private void OnEnable()
    {
        healthStringEvent.oneStringEvent += SetBaseHealthText;
        waveStringEvent.twoStringEvent += SetWaveText;
        roundStringEvent.twoStringEvent += SetRoundText;

    }
    private void OnDisable()
    {
        healthStringEvent.oneStringEvent -= SetBaseHealthText;
        waveStringEvent.twoStringEvent -= SetWaveText;
        roundStringEvent.twoStringEvent -= SetRoundText;
    }
}
