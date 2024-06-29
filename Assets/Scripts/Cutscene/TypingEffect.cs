using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public string currentText;
    public string currentName;
    public bool isTyping = false;

    public void SetName(string name)
    {
        nameText.text = name;
    }
    public void SetText(string text)
    {
        dialogueText.text = text;
    }

    public void StartTyping(string name, string text, float speed)
    {
        if (isTyping)
        {
            StopAllCoroutines();
        }
        isTyping = true;
        currentText = text;
        currentName = name;
        nameText.text = currentName;
        StartCoroutine(TypeText(currentText, speed));
    }

    private IEnumerator TypeText(string text, float speed)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(speed);
        }
        isTyping = false;
    }
    public void SkipTyping()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentText;
            isTyping = false;
        }
    }
}
