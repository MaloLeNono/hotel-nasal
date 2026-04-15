using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TextClip : PlayableAsset, ITimelineClipAsset
{
    public string text;

    public ClipCaps clipCaps => ClipCaps.None;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextBehaviour>.Create(graph);
        playable.GetBehaviour().text = text;
        return playable;
    }
}