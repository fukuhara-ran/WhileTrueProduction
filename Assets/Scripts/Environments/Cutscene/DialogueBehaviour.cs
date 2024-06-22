using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueBehaviour : PlayableBehaviour
{
    public string dialogueText;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        DialogueManager dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(new List<string> { dialogueText });
        }
    }
}