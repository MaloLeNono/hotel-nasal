using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 0)]
public class Card : ScriptableObject
{
    public Rank Rank;
    public int Value;
    public Suit Suit;
    public Sprite Artwork;
    
    public string GetCardName()
    {
        string cardValue = Rank == Rank.Number 
            ? Value.ToString() 
            : Rank.ToString();
        
        return $"{cardValue} of {Suit}";
    }
}



