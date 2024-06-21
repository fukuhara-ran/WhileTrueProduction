using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DialogueClip : PlayableAsset
{
    public string dialogueLine;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

        DialogueBehaviour dialogueBehaviour = playable.GetBehaviour();
        dialogueBehaviour.dialogueLine = dialogueLine;

        return playable;
    }
}