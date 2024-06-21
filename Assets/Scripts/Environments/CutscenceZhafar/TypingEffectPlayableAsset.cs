using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class TypingEffectPlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<TypingEffect> typingEffect;
    public string dialogueText;
    public float typingSpeed = 0.05f;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TypingEffectPlayableBehaviour>.Create(graph);
        TypingEffectPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.typingEffect = typingEffect.Resolve(graph.GetResolver());
        behaviour.dialogueText = dialogueText;
        behaviour.typingSpeed = typingSpeed;
        return playable;
    }
}

public class TypingEffectPlayableBehaviour : PlayableBehaviour
{
    public TypingEffect typingEffect;
    public string dialogueText;
    public float typingSpeed;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (typingEffect != null)
        {
            typingEffect.StartTyping(dialogueText, typingSpeed);
        }
    }
}
