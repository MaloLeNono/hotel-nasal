using UnityEngine;

public class DestroyMoney : MonoBehaviour
{
    private void Start() => Wallet.DestroyInstance();
}
