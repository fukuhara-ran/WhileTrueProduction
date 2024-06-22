using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueMixerBehaviour : PlayableBehaviour
{
    private List<Playable> activePlayables = new List<Playable>();
    private DialogueManager dialogueManager;
    private string currentDialogue = "";

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        dialogueManager = playerData as DialogueManager;

        if (dialogueManager == null)
            return;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            if (playable.GetInputWeight(i) > 0f)
            {
                ScriptPlayable<DialogueBehaviour> inputPlayable = (ScriptPlayable<DialogueBehaviour>)playable.GetInput(i);
                DialogueBehaviour input = inputPlayable.GetBehaviour();

                if (!activePlayables.Contains(inputPlayable))
                {
                    activePlayables.Add(inputPlayable);
                    if (input.dialogueText != currentDialogue)
                    {
                        currentDialogue = input.dialogueText;
                        dialogueManager.StartDialogue(new List<string> { currentDialogue });
                    }
                }
            }
            else
            {
                ScriptPlayable<DialogueBehaviour> inputPlayable = (ScriptPlayable<DialogueBehaviour>)playable.GetInput(i);
                if (activePlayables.Contains(inputPlayable))
                {
                    activePlayables.Remove(inputPlayable);
                }
            }
        }

        if (activePlayables.Count == 0)
        {
            dialogueManager.EndDialogue();
            currentDialogue = "";
        }
    }
}
