using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lender : MonoBehaviour, IInteractable
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Canvas loanCanvas;
    [SerializeField] private Slider loanSlider;
    [SerializeField] private TextMeshProUGUI loanAmountIndicator;
    [SerializeField] private float compoundingTime;

    private void Update() => loanAmountIndicator.text = $"Montant du pret: {loanSlider.value}$";

    public void Interact()
    {
        inputActions.FindActionMap("Player").Disable();
        loanCanvas.enabled = true;
        loanSlider.value = loanSlider.minValue;
    }

    public void Return()
    {
        loanCanvas.enabled = false;
        inputActions.FindActionMap("Player").Enable();
    }

    public void Borrow()
    {
        int amount = (int)loanSlider.value;
        Wallet.Instance.money.Amount += amount;
        Wallet.Instance.loans.Add(new Loan(amount, MarketRate.Instance.interestRate, compoundingTime));
    }
}