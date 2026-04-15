using UnityEngine.Playables;

public class TextMixerBehaviour : PlayableBehaviour
{
    private string _currentText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (playerData is not CutsceneDialog display) return;

        int count = playable.GetInputCount();
        for (int i = 0; i < count; i++)
        {
            if (playable.GetInputWeight(i) <= 0) continue;
            var behaviour = ((ScriptPlayable<TextBehaviour>)playable.GetInput(i)).GetBehaviour();

            if (behaviour.text == _currentText) return;
            _currentText = behaviour.text;
            display.ShowLine(_currentText);
            return;
        }
    }
}