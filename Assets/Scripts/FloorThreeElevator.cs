using UnityEngine;

public class FloorThreeElevator : FloorSwitcher
{
    [SerializeField] private BoxCollider2D darkZone;
    
    public override void Interact()
    {
        if (darkZone.enabled)
        {
            DialogController.Instance.StartDialog("Il n'y a pas de courant...");
            return;
        }
        
        base.Interact();
    }
}