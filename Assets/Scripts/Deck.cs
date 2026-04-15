using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Deck : Singleton<Deck>
{
    [field: SerializeField] public List<Card> Cards { get; set; } = new();
    [SerializeField] private GameObject cardTemplate;
    [SerializeField] private Sprite hiddenCard;
    [SerializeField] private Transform uiTransform;
    [SerializeField] private GameObject mockDeck;
    [SerializeField] private float cardMoveTime;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.X)) return;
        
        Card pickedCard = DrawCard();
        DisplayCard(pickedCard, Vector2.zero, true);
    }

    public Card DrawCard()
    {
        if (Cards.Count == 0)
        {
            Card card = ScriptableObject.CreateInstance<Card>();
            card.Value = -1;
            card.Suit = Suit.Invalid;
            card.Rank = Rank.Invalid;
            return card;
        }
        
        Card pickedCard = Cards[Random.Range(0, Cards.Count)];
        Cards.Remove(pickedCard);
        return pickedCard;
    }

    public GameObject DisplayCard(Card card, Vector2 position, bool shown)
    {
        GameObject cardObject = Instantiate(cardTemplate, uiTransform);
        cardObject.name = card.GetCardName();

        cardObject.GetComponent<RectTransform>().anchoredPosition = mockDeck.GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(MoveCard(position, cardObject));
        cardObject.GetComponent<Image>().sprite = shown ? card.Artwork : hiddenCard;
        
        return cardObject;
    }

    private IEnumerator MoveCard(Vector2 position, GameObject card)
    {
        Vector2 velocity = Vector2.zero;
        if (!card.TryGetComponent(out RectTransform rectTransform)) yield break;
        while (rectTransform && Vector2.Distance(rectTransform.anchoredPosition, position) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(
                rectTransform.anchoredPosition,
                position,
                ref velocity,
                cardMoveTime,
                Mathf.Infinity,
                Time.deltaTime
            );
            
            yield return null;
        }

        if (!rectTransform) yield break;
        rectTransform.anchoredPosition = position;
    }
}