using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    int CurrentDialogue = 0;
    public Dialogue[] dialogue;


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagment>().StartDialogue(dialogue[CurrentDialogue], dialogue[CurrentDialogue].onEnd);
        CurrentDialogue++;
    }

}
