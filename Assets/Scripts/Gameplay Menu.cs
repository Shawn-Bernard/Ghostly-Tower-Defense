using UnityEngine;
using TMPro;
public class GameplayMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baseHealth;
    [SerializeField] private StringEvent healthStringEvent;

    [SerializeField] private TextMeshProUGUI towerInfo;
    [SerializeField] private StringEvent towerStringEvent;

    [SerializeField] private TextMeshProUGUI roundInfo;
    [SerializeField] private StringEvent roundStringEvent;

    private void SetBaseHealthText(string health)
    {
        baseHealth.text = $"Base Health : {health}";
    }

    private void SetWaveText(string currentTowers,string totalTowers)
    {
        towerInfo.text = $"Tower {currentTowers}/{totalTowers}";
    }

    private void SetRoundText(string currentRound, string totalRounds)
    {
        roundInfo.text = $"Round {currentRound}/{totalRounds}";
    }

    private void OnEnable()
    {
        healthStringEvent.oneStringEvent += SetBaseHealthText;
        towerStringEvent.twoStringEvent += SetWaveText;
        roundStringEvent.twoStringEvent += SetRoundText;

    }
    private void OnDisable()
    {
        healthStringEvent.oneStringEvent -= SetBaseHealthText;
        towerStringEvent.twoStringEvent -= SetWaveText;
        roundStringEvent.twoStringEvent -= SetRoundText;
    }
}
