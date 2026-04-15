using TMPro;
using UnityEngine;

public class FloorDisplay : Singleton<FloorDisplay>
{
    private static readonly int Show = Animator.StringToHash("Show");
    
    [SerializeField] private TextMeshProUGUI floorNumberUi;
    [SerializeField] private TextMeshProUGUI floorNameUi;

    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    public void DisplayFloorInfo(uint floorNumber, string floorName)
    {
        floorNumberUi.text = $"== Étage {floorNumber} ==";
        floorNameUi.text = $"- {floorName}";
        _animator.SetTrigger(Show);
    }
}
