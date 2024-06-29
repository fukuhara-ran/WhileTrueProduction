using System;
using System.Collections.Generic;

public class Conversation
{
    private Queue<Dialog> dialogs;
    private string audioTrack;
    public string AudioTrack
    {
        get { return audioTrack; }
        set { audioTrack = value; }
    }
    public Conversation(string audioTrack = "")
    {
        dialogs = new Queue<Dialog>();
        this.audioTrack = audioTrack;
    }
    public void Enqueue(Dialog dialog)
    {
        dialogs.Enqueue(dialog);
    }
    public void Enqueue(string name, string text, float time = 10f)
    { 
        dialogs.Enqueue(new Dialog { Name = name, Text = text, Speed = time });
    }
    public Dialog Dequeue()
    {
        return dialogs.Dequeue();
    }
    public bool IsEmpty()
    {
        return dialogs.Count == 0;
    }
    public void Clear()
    {
        dialogs.Clear();
    }
}