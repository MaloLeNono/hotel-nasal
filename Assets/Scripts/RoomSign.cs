using Interfaces;
using UnityEngine;

public class RoomSign : MonoBehaviour, IInteractable
{
    private string _text;
    
    private void Awake()
    {
        int roomNumber = Random.Range(0, 20000);
        _text = $"Chambre {roomNumber}";
    }

    public void Interact() => DialogController.Instance.StartDialog(_text);
}