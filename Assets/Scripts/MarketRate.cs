using TMPro;
using UnityEngine;

public class MarketRate : Singleton<MarketRate>
{
    [SerializeField] private TextMeshProUGUI interestIndicator;
    [SerializeField] private float minInterest;
    [SerializeField] private float maxInterest;
    [SerializeField] private float interestUpdateTime;
    [SerializeField] private float interestVariationRate;
    
    [HideInInspector] public float interestRate;
    
    private bool _interestRising;
    
    protected override void Awake()
    {
        base.Awake();
        InvokeRepeating(nameof(UpdateInterest), 0f, interestUpdateTime);
    }
    
    private void Update()
    {
        float interestMultiplier = _interestRising
            ? 1f 
            : -1f;

        interestRate += interestVariationRate * interestMultiplier * Time.deltaTime;
        interestRate = Mathf.Clamp(interestRate, minInterest, maxInterest);
        interestIndicator.text = $"Taux d'interets: {interestRate:F1}%";
    }
    
    private void UpdateInterest() => _interestRising = Random.value > 0.5f;
}