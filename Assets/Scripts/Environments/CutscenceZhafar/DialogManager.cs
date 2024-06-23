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
    private bool isInitial = false;
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
        conversation = new Conversation();
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
    public void StopDialogue()
    {
        typingEffect.SetText("");
        typingEffect.SetName("");
        conversation.Clear();
    }
    public void Update()
    {
        if (conversation.IsEmpty())
        {
            if (!typingEffect.isTyping)
            {
                Debug.Log("End of conversation");
            }
            return;
        }

        if (isInitial)
        {
            NextDialog();
            isInitial = false;
            return;
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
                delayBetweenDialogs = 1f;
            }
        }
        else if (!typingEffect.isTyping && !conversation.IsEmpty())
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