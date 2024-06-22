using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    private TMP_Text dialogueText;
    private string fullText;
    private Coroutine typingCoroutine;
    private bool isTyping;

    private void Awake()
    {
        dialogueText = GetComponent<TMP_Text>();
    }

    public void StartTyping(string text)
    {
        fullText = text;
        isTyping = true;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText());
    }

    public void SkipTyping()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = fullText;
            isTyping = false;
        }
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            dialogueText.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
}
