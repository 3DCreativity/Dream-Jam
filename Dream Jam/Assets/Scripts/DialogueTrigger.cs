using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent onEnd;

    private void Awake()
    {
        if (onEnd == null)
        {
            onEnd = new UnityEvent();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagment>().StartDialogue(dialogue, onEnd);
    }

}
