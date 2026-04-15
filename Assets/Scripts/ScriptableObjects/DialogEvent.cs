using Abstract;
using ScriptableObjects.Dialog;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DialogEvent", menuName = "Events/Dialog Event")]
    public class DialogEvent : GameEvent
    {
        [SerializeField] private DialogSectionData dialog;
        
        public override void Execute()
        {
            DialogController.Instance.StartDialog(dialog);
        }
    }
}