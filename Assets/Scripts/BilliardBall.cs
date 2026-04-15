using Interfaces;
using UnityEngine;

public class BilliardBall : MonoBehaviour, IInteractable
{
    [SerializeField] private string ballName;
    
    public void Interact()
    {
        BilliardInventory.Instance.BilliardBalls.AddItem(this);
        if (BilliardInventory.Instance.BilliardBalls.Count < BilliardInventory.Instance.BilliardBalls.MaxItems)
            DialogController.Instance.StartDialog(
                new []
                {
                    $"J'ai trouvé la {ballName}!",
                    "Je me demande si je pourrais trouver toutes les boules de billard..."
                });
        Destroy(gameObject);
    }
}