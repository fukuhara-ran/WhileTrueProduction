using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class DialogManager : MonoBehaviour
{
    public enum DialogState
    {
        Idle,
        Typing,
        WaitingForInput,
        Transitioning
    }

    public TypingEffect typingEffect;
    public float typingSpeed = 0.05f;
    public float delayBetweenDialogs = 2f;
    public Dialog currentDialog;
    private Conversation conversation;
    [SerializeField] private DialogState currentState = DialogState.Idle;
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
        currentState = DialogState.Idle;
    }

    public void StartNewDialogueSequence()
    {
        typingEffect.SetText("");
        typingEffect.SetName("");
        currentState = DialogState.Transitioning;
    }

    public void StopDialogue()
    {
        typingEffect.SetText("");
        typingEffect.SetName("");
        conversation.Clear();
        currentState = DialogState.Idle;
    }

    public void Update()
    {
        switch (currentState)
        {
            case DialogState.Idle:
                if (!conversation.IsEmpty())
                {
                    //currentState = DialogState.Transitioning;
                }
                break;

            case DialogState.Typing:
                if (Input.GetMouseButtonDown(0))
                {
                    typingEffect.SkipTyping();
                    currentState = DialogState.WaitingForInput;
                }
                else if (!typingEffect.isTyping)
                {
                    currentState = DialogState.WaitingForInput;
                }
                break;

            case DialogState.WaitingForInput:
                if (Input.GetMouseButtonDown(0))
                {
                    currentState = DialogState.Transitioning;
                    delayBetweenDialogs = 2f;
                }
                else
                {
                    delayBetweenDialogs -= Time.deltaTime;
                    if (delayBetweenDialogs <= 0)
                    {
                        currentState = DialogState.Transitioning;
                        delayBetweenDialogs = 2f;
                    }
                }
                break;

            case DialogState.Transitioning:
                if (conversation.IsEmpty())
                {
                    Debug.Log("End of conversation");
                    currentState = DialogState.Idle;
                }
                else
                {
                    NextDialog();
                    currentState = DialogState.Typing;
                }
                break;
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