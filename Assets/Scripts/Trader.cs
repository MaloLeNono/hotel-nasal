using System.Collections.Generic;
using System.Linq;
using Interfaces;
using ScriptableObjects;
using ScriptableObjects.Dialog;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    private static readonly int Smile = Animator.StringToHash("Smile");
    private static readonly int Withdraw = Animator.StringToHash("Withdraw");
    
    [SerializeField] private DialogSectionData dialog;
    [SerializeField] private ParticleSystem coinParticles;

    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        dialog = Instantiate(dialog);
        dialog.dialogLines[0].firstResponse.action += () =>
        {
            
            _audioSource.Play();
            _animator.SetTrigger(Withdraw);
            List<ItemData> items = new();
            foreach (ItemData item in PlayerInventory.Instance.Items.Where(x => x.sellable))
            {
                Wallet.Instance.money.Amount += item.value;
                items.Add(item);
                coinParticles.Emit(item.value);
            }

            foreach (ItemData item in items) 
                PlayerInventory.Instance.RemoveItem(item);
        };
    }

    public void Interact()
    {
        if (!PlayerInventory.Instance.Items.Any(x => x.sellable))
        {
            DialogController.Instance.StartDialog("Je n'ai rien a vendre...");
            return;
        }
        
        _animator.SetTrigger(Smile);
        
        int money = PlayerInventory.Instance.Items.Sum(item => item.value);
        dialog.dialogLines[0].text = $"Voulez vous vendre vos objets? ({money}$)";
        
        DialogController.Instance.StartDialog(dialog);
    }
}