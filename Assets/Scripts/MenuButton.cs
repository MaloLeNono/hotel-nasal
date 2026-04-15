using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform background;
    [SerializeField] private MenuBackground menuBackground;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float scaleTime;
    
    private bool _pointerInsideButton;
    private Vector3 _velocity;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerInsideButton = true;
        menuBackground.currentBackground = background;
    }

    public void OnPointerExit(PointerEventData eventData) => _pointerInsideButton = false;

    private void Update()
    {
        Vector3 targetScale = _pointerInsideButton
            ? Vector3.one * scaleFactor
            : Vector3.one;

        transform.localScale = Vector3.SmoothDamp(
            transform.localScale,
            targetScale,
            ref _velocity,
            scaleTime,
            Mathf.Infinity,
            Time.deltaTime
        );
    }
}