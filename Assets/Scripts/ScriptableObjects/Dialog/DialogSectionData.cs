using DataClasses;
using UnityEngine;

namespace ScriptableObjects.Dialog
{
    [CreateAssetMenu(fileName = "DialogSectionData", menuName = "Dialog/Dialog Data", order = 0)]
    public class DialogSectionData : ScriptableObject
    {
        public DialogLine[] dialogLines;
    }
}