using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BlackJack : Singleton<BlackJack>
{
    [SerializeField] private GameObject blackJackUi;
    
    [Header("Controls")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Button standButton;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Slider betSlider;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerSum;
    [SerializeField] private TextMeshProUGUI dealerSum;
    [SerializeField] private TextMeshProUGUI betIndicator;
    
    [Header("Config")]
    [SerializeField] private Vector2 playerCardsPosition;
    [SerializeField] private Vector2 dealerCardsPosition;
    [SerializeField] private float cardSpacing;
    [SerializeField] private float endDelay;
    [SerializeField] private AudioClip music;
    
    private Card _holeCard;
    private GameObject _holeCardObject;
    private Vector2 _playerCardsCurrentPosition;
    private Vector2 _dealerCardsCurrentPosition;
    private int _bet;
    private bool _isResetting;

    private readonly Hand _playerHand = new();
    private readonly Hand _dealerHand = new();

    public bool PlayerHasPlayed { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        
        _playerCardsCurrentPosition = playerCardsPosition;
        _dealerCardsCurrentPosition = dealerCardsPosition;
    }

    private void Start()
    {
        standButton.onClick.AddListener(OnStand); 
        hitButton.onClick.AddListener(OnHit);
        startButton.onClick.AddListener(OnDeal);
        PlayButtonsInteractable(false);
    }

    private void Update()
    {
        betIndicator.text = $"Mise: {betSlider.value}$";
    }

    private void OnStand()
    {
        _holeCardObject.GetComponent<Image>().sprite = _holeCard.Artwork;
        dealerSum.text = _dealerHand.Sum.ToString();

        StartCoroutine(DealerOnStand());
    }

    private void OnHit()
    {
        _playerHand.AddCards(1);
        playerSum.text = _playerHand.Sum.ToString();
        Deck.Instance.DisplayCard(_playerHand.Cards[^1], _playerCardsCurrentPosition, true);
        _playerCardsCurrentPosition += Vector2.right * cardSpacing;
        
        CheckWinOrLose(false);
    }

    private void OnDeal()
    {
        _bet = (int)betSlider.value;
        Wallet.Instance.money.Amount -= _bet;
        DealFirstCards();
        playerSum.gameObject.SetActive(true);
        dealerSum.gameObject.SetActive(true);
        startButton.interactable = false;
        betSlider.interactable = false;
        PlayButtonsInteractable(true);
    }

    private void DealFirstCards()
    {
        dealerSum.text = "?";
        
        _dealerHand.AddCards(2);
        _playerHand.AddCards(2);
        playerSum.text = _playerHand.Sum.ToString();
        
        _holeCard = _dealerHand.Cards[1];
        
        Deck.Instance.DisplayCard(_dealerHand.Cards[0], _dealerCardsCurrentPosition, true);
        _holeCardObject = Deck.Instance.DisplayCard(_dealerHand.Cards[1], _dealerCardsCurrentPosition += Vector2.right * cardSpacing, false);
        
        foreach (Card card in _playerHand.Cards)
        {
            Deck.Instance.DisplayCard(card, _playerCardsCurrentPosition, true);
            _playerCardsCurrentPosition += Vector2.right * cardSpacing;
        }

        if (_dealerHand.Sum != 21) return;
        
        _holeCardObject.GetComponent<Image>().sprite = _holeCard.Artwork;
        dealerSum.text = _dealerHand.Sum.ToString();
        Push();
    }

    private void PlayButtonsInteractable(bool state)
    {
        standButton.interactable = state;
        hitButton.interactable = state;
    }

    private void Bust()
    {
        if (_isResetting) return;
        _isResetting = true;
        
        PlayButtonsInteractable(false);
        StopAllCoroutines();
        StartCoroutine(ResetGame());
    }

    private void Win()
    {
        if (_isResetting) return;
        _isResetting = true;
        
        Wallet.Instance.money.Amount += _bet;
        if (_playerHand.IsBlackJack())
        {
            float moneyToAdd = _bet * (3f / 2f);
            Wallet.Instance.money.Amount += (int)moneyToAdd;
        }
        else
            Wallet.Instance.money.Amount += _bet;

        PlayButtonsInteractable(false);
        StopAllCoroutines();
        StartCoroutine(ResetGame());
    }
    
    private void Push()
    {
        if (_isResetting) return;
        _isResetting = true;
        
        Wallet.Instance.money.Amount += _bet;
        PlayButtonsInteractable(false);
        StopAllCoroutines();
        StartCoroutine(ResetGame());
    }

    private IEnumerator DealerOnStand()
    {
        yield return new WaitForSeconds(0.4f);

        _dealerCardsCurrentPosition += Vector2.right * cardSpacing;
        
        while (_dealerHand.Sum < 17)
        {
            _dealerHand.AddCards(1);
            Deck.Instance.DisplayCard(_dealerHand.Cards[^1], _dealerCardsCurrentPosition, true);
            _dealerCardsCurrentPosition += Vector2.right * cardSpacing;
            dealerSum.text = _dealerHand.Sum.ToString();

            yield return new WaitForSeconds(0.4f);
        }
        
        CheckWinOrLose(true);
    }

    private void CheckWinOrLose(bool playerStood)
    {
        if (_playerHand.Sum > 21) 
            Bust();
        else if (_dealerHand.Sum > 21) 
            Win();
        else switch (playerStood)
        {
            case true when _playerHand.Sum < _dealerHand.Sum:
                Bust();
                break;
            case true when _playerHand.Sum > _dealerHand.Sum:
                Win();
                break;
            case true when _playerHand.Sum == _dealerHand.Sum:
                Push();
                break;
        }
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(endDelay);
        
        MusicController.Instance.RevertSong();
        
        inputActionAsset.FindActionMap("Player").Enable();
        
        ResetHands();
        
        _dealerCardsCurrentPosition = dealerCardsPosition;
        _playerCardsCurrentPosition = playerCardsPosition;
        
        _holeCard = null;
        _holeCardObject = null;
        
        dealerSum.gameObject.SetActive(false);
        playerSum.gameObject.SetActive(false);
        
        var displayedCards = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject card in displayedCards)
            Destroy(card);
        
        PlayButtonsInteractable(false);
        blackJackUi.SetActive(false);
        
        _isResetting = false;
    }

    private void ResetHands()
    {
        Deck.Instance.Cards.AddRange(_playerHand.Cards);
        Deck.Instance.Cards.AddRange(_dealerHand.Cards);
        
        _playerHand.Reset();
        _dealerHand.Reset();
    }

    public void SwitchToGame()
    {
        PlayerHasPlayed = true;
        MusicController.Instance.SwitchSong(music);
        inputActionAsset.FindActionMap("Player").Disable();
        blackJackUi.SetActive(true);
        startButton.interactable = true;
        betSlider.interactable = true;
        PlayButtonsInteractable(false);
        betSlider.minValue = 1f;
        betSlider.maxValue = Wallet.Instance.money.Amount;
    }
}
