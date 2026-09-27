using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] private TextMeshProUGUI coinInfo;

    private void Awake()
    {
        coinInfo = GetComponent<TextMeshProUGUI>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinInfo.text = $"{weapon.GetCost()}";
    }
}
