using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class DialogManager : MonoBehaviour
{
    public TypingEffect typingEffect;
    public float typingSpeed = 0.05f;
    public Dialog currentDialog;
    private Conversation conversation;
    public void Start()
    {
        conversation = new Conversation();
        conversation.Enqueue("Justin", "For all the times that you rain on my parade");
        conversation.Enqueue("Girlfriend", "And all the clubs you get in using my name");
        conversation.Enqueue("Justin", "You think you broke my heart, oh girl for goodness sake");
        conversation.Enqueue("Girlfriend", "You think I'm crying on my own, well I ain't");
        conversation.Enqueue("Justin", "And I didn't wanna write a song");
        conversation.Enqueue("Justin", "'Cause I didn't want anyone thinking I still care, I don't");
        conversation.Enqueue("Girlfriend", "But you still hit my phone up");
        conversation.Enqueue("Justin", "And baby, I'll be movin' on");
        conversation.Enqueue("Justin", "And I think you should be somethin' I don't wanna hold back");
        conversation.Enqueue("Girlfriend", "Maybe you should know that");
        conversation.Enqueue("Justin", "My mama don't like you and she likes everyone");
        currentDialog = conversation.Dequeue();
    }
    public void Update()
    {
        switch (typingEffect.isTyping)
        {
            case true:
                if (Input.GetMouseButtonDown(0))
                    typingEffect.SkipTyping();
                break;
            case false:
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentDialog != null)
                    {
                        typingEffect.StartTyping(currentDialog.Name, currentDialog.Text, typingSpeed);
                        currentDialog = conversation.Dequeue();
                    }
                }
                break;
        }
    }
}