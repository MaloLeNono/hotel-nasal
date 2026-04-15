using System;
using Abstract;
using ScriptableObjects.Dialog;

namespace DataClasses
{
    [Serializable]
    public class Response
    {
        public string responseOption;
        public GameEvent responseEvent;
        public DialogSectionData answerDialogSection;
        public bool keepControlsOff;
        public Action action;
    }
}