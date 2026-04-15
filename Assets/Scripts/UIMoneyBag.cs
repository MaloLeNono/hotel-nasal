using UnityEngine;
using UnityEngine.UI;

public class UIMoneyBag : MonoBehaviour
{

    [SerializeField] private Sprite stageOne;
    [SerializeField] private Sprite stageTwo;
    [SerializeField] private Sprite stageThree;
    [SerializeField] private Sprite stageFour;
    [SerializeField] private float stageOneEnd;
    [SerializeField] private float stageTwoEnd;
    [SerializeField] private float stageThreeEnd;

    private Image _image;

    private void Awake() => _image = GetComponent<Image>();
    
    private void Update()
    {
        if (MoneyIsBetween(0f, stageOneEnd))
            _image.sprite = stageOne;
        else if (MoneyIsBetween(stageOneEnd, stageTwoEnd))
            _image.sprite = stageTwo;
        else if (MoneyIsBetween(stageTwoEnd, stageThreeEnd))
            _image.sprite = stageThree;
        else
            _image.sprite = stageFour;
    }

    private static bool MoneyIsBetween(float minInclusive, float maxExclusive)
    {
        return Wallet.Instance.money.Amount >= minInclusive && 
               Wallet.Instance.money.Amount < maxExclusive;
    }
}
