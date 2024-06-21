using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.05f;
    private bool isTyping = false;
    private bool canGoToNextDialogue = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canGoToNextDialogue)
        {
            GoToNextDialogue();
        }
    }

    public IEnumerator TypeLine(string line)
    {
        isTyping = true;
        canGoToNextDialogue = false;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        canGoToNextDialogue = true;
    }

    private void GoToNextDialogue()
    {
        // Implement logic to proceed to the next dialogue
        Debug.Log("Next dialogue can be triggered from the timeline.");
    }

    public void ShowDialogue(string line)
    {
        StartCoroutine(TypeLine(line));
    }
}