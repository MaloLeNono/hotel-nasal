using System;
using UnityEngine;

[Serializable]
public class Loan
{
    [field: SerializeField] public float AmountToPay { get; private set; }
    public float interestRate;
    public int originalSum;
    public float compoundingPeriod;

    private float _timer;
    
    public Loan(int sum, float interest, float period)
    {
        compoundingPeriod = period;
        interestRate = interest;
        originalSum = sum;
        AmountToPay = sum;
        Wallet.Instance.debt += sum;
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < compoundingPeriod) return;
        
        float sumToAdd = originalSum * interestRate / 100f;
        AmountToPay += sumToAdd;
        Wallet.Instance.debt += sumToAdd;
        _timer = 0f;
    }
}