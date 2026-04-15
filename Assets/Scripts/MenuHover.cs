using UnityEngine;
using UnityEngine.UI;

public class MenuHover : MonoBehaviour
{
    [SerializeField] private Sprite background;
    
    private Image _backgroundImage;

    private void Awake() => _backgroundImage = GameObject.FindWithTag("Background").GetComponent<Image>();

    public void OnHover()
    {
        _backgroundImage.sprite = background;
    }
}
