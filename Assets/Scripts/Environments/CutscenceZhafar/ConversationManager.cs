using UnityEngine;
using System;

public class ConversationManager : MonoBehaviour
{
    public DialogManager dialogManager;
    public void SetConversation(string id)
    {
        switch (id)
        {
            case "bab1a":
                Bab1a();
                break;
            case "bab2":
                Bab2();
                break;
            default:
                break;
        }
    }
    public void Bab1a()
    {
        Conversation conversation = new Conversation();
        conversation.Enqueue("Ash", "Who's there?");
        conversation.Enqueue("Ash", "Show yourself! you fucking bitch!");
        dialogManager.LoadConversation(conversation);
    }
    public void Bab2()
    {
        Conversation conversation = new Conversation();
        conversation.Enqueue("Helena", "Rasanya seperti hari yang sempurna untuk petualangan, bukan?");
        conversation.Enqueue("Ash", "Ya, Helena. Hari ini adalah hari yang sempurna untuk petualangan.");
        dialogManager.LoadConversation(conversation);
    }
}