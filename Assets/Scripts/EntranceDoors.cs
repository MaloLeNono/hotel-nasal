using Interfaces;
using ScriptableObjects;
using ScriptableObjects.Dialog;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceDoors : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogSectionData unlockDialog;
    
    private ItemData _keyItem;
    private ItemData _torchItem;
    
    private void Awake()
    {
        _keyItem = Resources.Load<ItemData>("Items/Key");
        _torchItem = Resources.Load<ItemData>("Items/Torch");
        
        unlockDialog = Instantiate(unlockDialog);
        unlockDialog.dialogLines[1].firstResponse.action = () =>
        {
            SceneManager.LoadScene(PlayerInventory.Instance.Items.Contains(_torchItem)
                ? "BestEnding"
                : "NeutralEnding"
            );
        };
    }

    public void Interact()
    {
        if (!PlayerInventory.Instance.Items.Contains(_keyItem))
        {
            DialogController.Instance.StartDialog("Les portes sont barrées à clé... Peut-être qu'il y a une clé quelque part?");
            return;
        }
        
        DialogController.Instance.StartDialog(unlockDialog);
    }
}
