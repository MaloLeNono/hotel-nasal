using System;
using System.Timers;
using UnityEngine;

[Serializable]
public class Loan
{
    [field: SerializeField] public float AmountToPay { get; private set; }
    public float interestRate;
    public int originalSum;
    public float coumpoundingPeriod;

    public Loan(int sum, float interest, float period)
    {
        coumpoundingPeriod = period;
        interestRate = interest;
        originalSum = sum;
        AmountToPay = sum;
        Wallet.Instance.debt += sum;
        Timer timer = new(coumpoundingPeriod * 1000.0);
        timer.Elapsed += (_, _) => CalculateInterest();
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    private void CalculateInterest()
    {
        float sumToAdd = originalSum * interestRate / 100f;
        AmountToPay += sumToAdd;
        Wallet.Instance.debt += sumToAdd;
    }
}