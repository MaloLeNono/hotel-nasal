using Abstract;
using ScriptableObjects.Dialog;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MoneyEvent", menuName = "Events/Money Event")]
    public class MoneyEvent : GameEvent
    {
        public int amount;
        
        public override void Execute() => Wallet.Instance.money.Amount += amount;
    }
}