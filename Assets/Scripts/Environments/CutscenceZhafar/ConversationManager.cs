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
            case "bab1b":
                Bab1b();
                break;
            case "bab1c":
                Bab1c();
                break;
            default:
                break;
        }
    }
    public void Bab1a()
    {
        Conversation conversation = new Conversation();
        conversation.Enqueue("Helena", "Our mission grows more challenging by the day, Ash. Yet, I find joy in facing it alongside you");
        conversation.Enqueue("Ash", "Indeed, Helena. Together we make an exceptional team.");
        conversation.Enqueue("Ash", "Wait, do you sense that? Something's not right...");
        conversation.Enqueue("Helena", "Ash, look out! There's movement in the shadows!");
        dialogManager.LoadConversation(conversation);
    }
    public void Bab1b()
    {
        Conversation conversation = new Conversation();
        conversation.Enqueue("Vargoth", "Ah, how delightful. Two brave adventurers, completely unaware of the doom that awaits them.");
        conversation.Enqueue("Helena", "What do you want from us?");
        conversation.Enqueue("Vargoth", "I desire power, chaos, and the suffering of those who dare to oppose me.");
        conversation.Enqueue("Helena", "Whatever your aims are, we won't allow you to bring harm to these lands or its people.");
        conversation.Enqueue("Ash", "Allow? My dear, you speak as if you have a choice in the matter. Your determination is admirable, if woefully misplaced.");
        dialogManager.LoadConversation(conversation);
    }
    public void Bab1c()
    {
        Conversation conversation = new Conversation();
        conversation.Enqueue("Ash", "Helena, where are you?");
        conversation.Enqueue("Vargoth's Echo", "Your cries are in vain, young hero. She is beyond your reach now.");
        conversation.Enqueue("Ash", "I don't care what dark powers you wield, Vargoth. I will find you, and I will save her.");
        conversation.Enqueue("Ash", "Hold on, Helena. No matter what trials lie ahead, I swear I'll find you. Our bond is stronger than any darkness.");
        dialogManager.LoadConversation(conversation);
    }
}