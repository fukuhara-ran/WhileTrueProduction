using UnityEngine;
using System;

public class ConversationManager : MonoBehaviour
{
    public DialogManager dialogManager;
    public void SetConversation(string id)
    {
        switch (id)
        {
            case "bab1":
                Bab1();
                break;
            case "bab2":
                Bab2();
                break;
            default:
                break;
        }
    }
    public void Bab1()
    {
        Conversation conversation = new Conversation();
        conversation.Enqueue("Helena", "I feel like our mission is getting more challenging, Ash. But I'm glad I could do it with you.");
        conversation.Enqueue("Ash", "I feel the same way, Helena. I'm glad you're here with me.");
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