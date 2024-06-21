using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueBehaviour : PlayableBehaviour
{
    public string dialogueLine;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        GameObject directorGameObject = (playable.GetGraph().GetResolver() as PlayableDirector).gameObject;
        DialogueSystem dialogueSystem = directorGameObject.GetComponent<DialogueSystem>();

        if (dialogueSystem != null)
        {
            dialogueSystem.ShowDialogue(dialogueLine);
        }
    }
}
