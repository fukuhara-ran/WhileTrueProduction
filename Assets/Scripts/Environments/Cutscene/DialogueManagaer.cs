using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text characterNameText; // New TMP_Text component for character name
    public TMP_Text dialogueText;
    private Queue<DialogueLine> dialogueQueue;
    private TypewriterEffect typewriterEffect;
    private bool isTyping;

    private void Awake()
    {
        dialogueQueue = new Queue<DialogueLine>();
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

    public void StartDialogue(List<DialogueLine> dialogues)
    {
        dialogueQueue.Clear();

        foreach (DialogueLine dialogue in dialogues)
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

        DialogueLine dialogueLine = dialogueQueue.Dequeue();
        characterNameText.text = dialogueLine.characterName;
        isTyping = true;
        typewriterEffect.StartTyping(dialogueLine.dialogueText);
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        characterNameText.text = "";
    }
}

// Struct to store dialogue and character name
public struct DialogueLine
{
    public string characterName;
    public string dialogueText;

    public DialogueLine(string characterName, string dialogueText)
    {
        this.characterName = characterName;
        this.dialogueText = dialogueText;
    }
}