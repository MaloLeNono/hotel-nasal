using System;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class CreditsSequence
    {
        public CanvasGroup sequenceCanvasGroup;
        public float fadeInTime;
        public float stayTime;
        public float fadeOutTime;
    }
}