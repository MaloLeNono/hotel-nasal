using Interfaces;
using ScriptableObjects.Dialog;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BreakerBox : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogSectionData dialog;
    [SerializeField] private Trap[] trapsToDisable;

    private bool _deactivated;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        dialog = Instantiate(dialog);
        dialog.dialogLines[0].firstResponse.action = () =>
        {
            _audioSource.Play();
            
            foreach (Trap trap in trapsToDisable) 
                trap.activated = false;
            
            _deactivated = true;
        };
    }

    public void Interact()
    {
        if (_deactivated)
        {
            DialogController.Instance.StartDialog("L'électricité est déjà fermée.");
            return;
        }
        
        DialogController.Instance.StartDialog(dialog);
    }
}