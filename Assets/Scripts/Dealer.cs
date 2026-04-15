using Interfaces;
using ScriptableObjects.Dialog;
using UnityEngine;

public class Dealer : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogSectionData firstTimeDialog;
    [SerializeField] private DialogSectionData promptDialog;

    private bool _firstTimeSpeaking = true;

    public void Interact()
    {
        if (Wallet.Instance.money.Amount <= 0)
            DialogController.Instance.StartDialog("Je ne devrais probablement pas plus m'endetter...");
        else if (BlackJack.Instance.PlayerHasPlayed && !_firstTimeSpeaking)
            DialogController.Instance.StartDialog(promptDialog);
        else
            DialogController.Instance.StartDialog(firstTimeDialog);

        _firstTimeSpeaking = false;
    }
}