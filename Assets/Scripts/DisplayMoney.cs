using TMPro;
using UnityEngine;

public class DisplayMoney : MonoBehaviour
{
    private TextMeshProUGUI _text;
    
    private void Awake() => _text = GetComponent<TextMeshProUGUI>();
    private void Update() => _text.text = $"{Wallet.Instance.money.Amount:C}";
}
