using System.Collections;
using System.Globalization;
using Interfaces;
using ScriptableObjects.Dialog;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour, IInteractable
{
    [SerializeField] private int switchPrice;
    [SerializeField] private Transform floorToGo;
    [SerializeField] private string floorName;
    [SerializeField] private uint floorNumber;
    [SerializeField] private DialogSectionData dialog;
    [SerializeField] private Animator overlayAnimator;
    [SerializeField] private GameObject currentFloor;
    [SerializeField] private GameObject nextFloor;
    [SerializeField] private AudioSource audioSource;

    private Transform _player;

    private void Awake()
    {
        dialog = Instantiate(dialog);
        dialog.dialogLines[0].text += $" ({switchPrice.ToString("C", new CultureInfo("en-US"))})";
        dialog.dialogLines[0].firstResponse.action += () => StartCoroutine(SwitchFloor());
        _player =  GameObject.FindWithTag("Player").transform;
    }

    public virtual void Interact()
    {
        if (Wallet.Instance.money.Amount < switchPrice)
            DialogController.Instance.StartDialog(
                $"Je n'ai pas assez d'argent pour aller au prochain étage... Il me faut {switchPrice.ToString("C", new CultureInfo("en-US"))} pour accéder à l'ascenseur.");
        else
            DialogController.Instance.StartDialog(dialog);
    }

    private IEnumerator SwitchFloor()
    {
        Wallet.Instance.money.Amount -= switchPrice;
        overlayAnimator.Play("SwitchFloor", 0, 0f);
        audioSource.Play();
        nextFloor.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        _player.position = floorToGo.position;
        currentFloor.SetActive(false);
        FloorDisplay.Instance.DisplayFloorInfo(floorNumber, floorName);
    }
}