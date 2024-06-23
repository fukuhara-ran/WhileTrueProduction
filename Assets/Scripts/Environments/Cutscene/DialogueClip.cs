using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueClip : PlayableAsset
{
    public string characterName;
    public string dialogueText;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

        var dialogueBehaviour = playable.GetBehaviour();
        dialogueBehaviour.characterName = characterName;
        dialogueBehaviour.dialogueText = dialogueText;

        return playable;
    }
}