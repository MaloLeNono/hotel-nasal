using System.Globalization;
using TMPro;
using UnityEngine;

public class DisplayDebt : MonoBehaviour
{
    private TextMeshProUGUI _display;
    
    private void Awake() => _display = GetComponent<TextMeshProUGUI>();

    private void Update() => _display.text = $"Dettes: {Wallet.Instance.debt.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}";
}