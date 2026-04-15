using Interfaces;
using ScriptableObjects.Dialog;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PowerSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogSectionData interactDialog;
    [SerializeField] private BoxCollider2D darkZoneTrigger;
    [SerializeField] private AudioReverbZone reverbZone;

    private bool _switched;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        interactDialog = Instantiate(interactDialog);
        interactDialog.dialogLines[0].firstResponse.action += () =>
        {
            _audioSource.Play();
            darkZoneTrigger.enabled = false;
            reverbZone.enabled = false;
            _switched = true;
        };
    }

    public void Interact()
    {
        if (_switched)
        {
            DialogController.Instance.StartDialog("J'ai deja restaure le courant.");
            return;
        }
        
        DialogController.Instance.StartDialog(interactDialog);
    }
}