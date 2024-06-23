using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class DialogManager : MonoBehaviour
{
    public TypingEffect typingEffect;
    public float typingSpeed = 0.05f;
    public float delayBetweenDialogs = 1f;
    public Dialog currentDialog;
    private Conversation conversation;
    public bool isInitial = false;
    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        LoadDefaultConversation();
    }
    public void LoadDefaultConversation()
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
        conversation.Enqueue("Girlfriend", "And I never like to admit that I was wrong");
        conversation.Enqueue("Justin", "And I've been so caught up in my job, didn't see what's going on");
        conversation.Enqueue("Girlfriend", "But now I know, I'm better sleeping on my own");
    }
    public void LoadConversation(Conversation newConversation)
    {
        conversation = newConversation;
    }

    public void StartNewDialogueSequence()
    {
        typingEffect.SetText("");
        typingEffect.SetName("");
        isInitial = true;
    }
    public void Update()
    {
        if (conversation.IsEmpty() && !typingEffect.isTyping)
        {
            Debug.Log("End of conversation");
            return;
        }

        if (isInitial)
        {
            NextDialog();
            isInitial = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (typingEffect.isTyping)
            {
                typingEffect.SkipTyping();
            }
            else
            {
                NextDialog();
            }
        }
        if (!typingEffect.isTyping && !conversation.IsEmpty())
        {
            delayBetweenDialogs -= Time.deltaTime;
            if (delayBetweenDialogs <= 0)
            {
                NextDialog();
                delayBetweenDialogs = 1f;
            }
        }
    }

    private void NextDialog()
    {
        if (!conversation.IsEmpty())
        {
            currentDialog = conversation.Dequeue();
            typingEffect.StartTyping(currentDialog.Name, currentDialog.Text, typingSpeed);
        }
    }
}