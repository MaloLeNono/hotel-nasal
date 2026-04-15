using Abstract;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BlackJackEvent", menuName = "Events/BlackJack Event")]
    public class BlackJackEvent : GameEvent
    {
        public override void Execute() => BlackJack.Instance.SwitchToGame();
    }
}