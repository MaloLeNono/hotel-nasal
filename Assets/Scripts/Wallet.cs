using System.Collections.Generic;

public class Wallet : Singleton<Wallet>
{
    public Money money;
    public float debt;
    public List<Loan> loans = new();

    protected override void Awake()
    {
        base.Awake();

        bool success = JsonFileLoader.LoadFile("money.json", out money);

        if (success) return;
        
        money = new Money(100);
        JsonFileLoader.SaveFile("money.json", money);
    }
}
