using System;
using UnityEngine;

[Serializable]
public class Money
{
    public event Action<int> OnMoneyChanged;
    [SerializeField] private int amount;
    public int Amount
    {
        get => amount;
        set
        {
            OnMoneyChanged?.Invoke(value - amount);
            
            if (value >= 0)
            {
                amount = value;
                return;
            }

            int debtToAdd = Mathf.Abs(value);
            amount = 0;
            Wallet.Instance.debt += debtToAdd;
        }
    }

    public Money(int amount) => Amount = amount;
}