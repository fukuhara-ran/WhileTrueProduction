using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    private Queue<string> dialogueQueue;
    private TypewriterEffect typewriterEffect;
    private bool isTyping;

    private void Awake()
    {
        dialogueQueue = new Queue<string>();
        typewriterEffect = dialogueText.GetComponent<TypewriterEffect>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                typewriterEffect.SkipTyping();
                isTyping = false;
            }
            else
            {
                DisplayNextDialogue();
            }
        }
    }

    public void StartDialogue(List<string> dialogues)
    {
        dialogueQueue.Clear();

        foreach (string dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string dialogue = dialogueQueue.Dequeue();
        isTyping = true;
        typewriterEffect.StartTyping(dialogue);
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
    }
}