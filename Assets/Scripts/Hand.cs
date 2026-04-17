using System.Collections.Generic;

public class Hand
{
    public readonly List<Card> Cards = new();
    public int Sum;
    
    public void AddCards(int amount)
    {
        if (Deck.Instance.Cards.Count == 0) return;

        for (int i = 0; i < amount; i++)
        {
            Card pickedCard = Deck.Instance.DrawCard();
            Cards.Add(pickedCard);

            if (pickedCard.Rank != Rank.Ace)
                Sum += pickedCard.Value;
            else
            {
                if (Sum + 11 > 21)
                    Sum += 1;
                else
                    Sum += 11;
            }
        }
    }

    public void Reset()
    {
        Cards.Clear();
        Sum = 0;
    }

    public bool IsBlackJack() => Cards.Count == 2 && Sum == 21;
}