using UnityEngine;

public class DestroyMoney : MonoBehaviour
{
    private void Start()
    {
        if (Wallet.Instance is null) return;
        Wallet.Instance.ResetWallet();
    }
}
