using ScriptableObjects.Dialog;
using UnityEngine;

public class BilliardInventory : Singleton<BilliardInventory>
{
    [SerializeField] private DialogSectionData completeDialog;
    
    public Inventory<BilliardBall> BilliardBalls;

    protected override void Awake()
    {
        base.Awake();
        BilliardBalls = new Inventory<BilliardBall>(16);
    }

    private void Update()
    {
        if (BilliardBalls.Count >= BilliardBalls.MaxItems)
            OnAllBallsCollected();
    }

    private void OnAllBallsCollected()
    {
        BilliardBalls.Items.Clear();
        DialogController.Instance.StartDialog(completeDialog);
    }
}
