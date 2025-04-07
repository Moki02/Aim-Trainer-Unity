using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;

public class KillFeed : MonoBehaviour 
{
    [SerializeField] TMP_Text killFeedText;
    private Queue<string> messages = new Queue<string>();
    private int maxMessages = 3;

    public void Log(string message)
    {
        messages.Enqueue(message);
        if (messages.Count > maxMessages)
        {
            messages.Dequeue();
        }
        
        UpdateKillFeedText();
    }

    void UpdateKillFeedText()
    {
        killFeedText.text = string.Join("\n", messages.ToArray());
    }
}