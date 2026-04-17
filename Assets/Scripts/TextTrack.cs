using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(TextClip))]
[TrackBindingType(typeof(CutsceneDialog))]
public class TextTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TextMixerBehaviour>.Create(graph, inputCount);
    }
}