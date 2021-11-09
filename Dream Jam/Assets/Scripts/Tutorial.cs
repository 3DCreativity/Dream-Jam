using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Player;
    public GameObject cat;
    public GameObject Executor;
    public GameObject Ghost;
    public float catSpeed = 1f;
    public DialogueTrigger dialogue;
    private CharacterController2D modify;

    private void Start()
    {
        Player = GameObject.Find("Player");
        cat = GameObject.Find("cat");
        Executor = GameObject.Find("Dead");
        Ghost = GameObject.Find("Training");
        dialogue = FindObjectOfType<DialogueTrigger>();
    }
    public void CatComing()
    {
        modify = cat.GetComponent<CharacterController2D>();
        modify.Move(-catSpeed,false,false);
    }
    public void CatCame()
    {
        dialogue.TriggerDialogue();
    }
    public void JourneyBegins()
    {

    }
    
    public void FirstBattle()
    {

    }
    public void SecondBattle()
    {

    }
    public void ThirdBattle()
    {

    }
    public void ExecutorBattle()
    {

    }
}
