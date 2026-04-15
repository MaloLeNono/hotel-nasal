using System.Collections.Generic;

public class Wallet : Singleton<Wallet>
{
    public Money money;
    public float debt;
    public List<Loan> loans = new();
    
    protected override void Awake()
    {
        base.Awake();
        money = new Money(100);
    }

    private void Update()
    {
        foreach (Loan loan in loans) 
            loan.Update();
    }

    private void OnDestroy() => loans.Clear();
}
