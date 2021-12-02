using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    int CurrentDialogue = 0;
    public Dialogue[] dialogue;

    private void OnEnable()
    {
        CurrentDialogue = GameObject.FindObjectOfType<DialogueManagment>().CurrentDialogue;
    }
    private void Update()
    {
        GameObject.FindObjectOfType<DialogueManagment>().CurrentDialogue = CurrentDialogue;
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagment>().StartDialogue(dialogue[CurrentDialogue], dialogue[CurrentDialogue].onEnd);
        CurrentDialogue++;
    }

}
