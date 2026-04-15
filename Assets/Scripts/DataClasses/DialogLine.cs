using System;
using Abstract;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class DialogLine
    {
        public string text;
        public GameEvent enterLineEvent;
        public Sprite expression;
        public float timeBetweenCharacters;
        public Response firstResponse;
        public Response secondResponse;
        public bool canRespond;
        public AudioClip voiceline;
        public GameEvent continueEvent;
        public Action action;
    }
}