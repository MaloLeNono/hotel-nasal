using UnityEngine;

namespace Abstract
{
    public abstract class GameEvent : ScriptableObject
    {
        public abstract void Execute();
    }
}