using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class TypingEffectPlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<TypingEffect> typingEffect;
    public ExposedReference<DialogManager> dialogManager;
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
        behaviour.dialogManager = dialogManager.Resolve(graph.GetResolver());
        behaviour.typingSpeed = typingSpeed;
        return playable;
    }
}

public class TypingEffectPlayableBehaviour : PlayableBehaviour
{
    public TypingEffect typingEffect;
    public DialogManager dialogManager;
    public string dialogueText;
    public string dialogueName;
    public float typingSpeed;
    private bool wasActive = false;

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (typingEffect == null || dialogManager == null) return;

        bool isActive = playable.GetGraph().IsPlaying() &&
                        playable.GetTime() >= 0 &&
                        playable.GetTime() <= playable.GetDuration();

        if (isActive)
        {
            float normalizedTime = (float)(playable.GetTime() / playable.GetDuration());
            int characterCount = Mathf.FloorToInt(normalizedTime * dialogueText.Length);
            string visibleText = dialogueText.Substring(0, Mathf.Clamp(characterCount, 0, dialogueText.Length));
            typingEffect.SetText(visibleText);
            typingEffect.SetName(dialogueName);
            wasActive = true;
        }
        else if (wasActive)
        {
            typingEffect.SetText("");
            typingEffect.SetName("");
            wasActive = false;
        }
    }

    public override void OnPlayableCreate(Playable playable)
    {
        if (dialogManager != null)
        {
            dialogueText = dialogManager.currentDialog.Text;
            dialogueName = dialogManager.currentDialog.Name;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (typingEffect != null && dialogManager != null)
        {
            dialogManager.typingEffect = typingEffect;
            dialogManager.typingSpeed = typingSpeed;
            dialogManager.isInitial = true;
        }
    }
}
