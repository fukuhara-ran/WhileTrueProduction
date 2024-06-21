using UnityEngine;
using TMPro;
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public void StartTyping(string text, float speed)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(text, speed));
    }

    private IEnumerator TypeText(string text, float speed)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
}
