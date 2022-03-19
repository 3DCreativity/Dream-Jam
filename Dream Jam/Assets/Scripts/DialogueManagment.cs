using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManagment : MonoBehaviour
{
    public Text mainText;
    public Text nameText;
    public Sprite[] faces;
    public Image Character;
    public Animator animator;
    public int CurrentDialogue = 0;
    Queue<string> sentences = new Queue<string>();
    UnityEvent onEnd;
    string Developer;
    bool startedDialogue = false;
    //[SerializeField]
    //private List<string> File;
    [SerializeField]
    private List<Dialogue> dialogue;
    [SerializeField]
    DialogueTrigger MainDialogue;
    [SerializeField]
    bool readFromFile = true;
    public Triggers[] triggers;

    private void Start()
    {
        if (onEnd == null)
        {
            onEnd = new UnityEvent();
        }
        Developer = GameObject.FindObjectOfType<GameManager>().developerName;
        if (readFromFile)
            GetDialogue();
    }
    //private void Update()
    //{
        
    //}
    public void StartDialogue(Dialogue dialogue, UnityEvent onEnding)
    {
        startedDialogue = true;
        animator.SetBool("isOpen", true);
        string[] characters = { "Magician", "Developer", "Cat", "Archer", "Ghost Executor", "Warrior", "Evil Wizard", "Evil Wizard 2" };
        bool foundFace = false;
        Character.sprite = faces[7];
        for (int i=0; foundFace == false && i<7; i++)
        {
            if (dialogue.photo == characters[i])
            {
                Character.sprite = faces[i];
                foundFace = true;
            }
        }

        nameText.text = dialogue.name;

        if ((dialogue.photo == "Developer" || dialogue.photo == "Magician") && nameText.text == "")
        {
            nameText.text = Developer;
        }
        if (sentences.Count != 0)
            sentences.Clear();

        onEnd = onEnding;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndCharacterDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        mainText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            mainText.text += letter;
            yield return null;
        }
    }
    void EndCharacterDialogue()
    {
        startedDialogue = false;
        onEnd.Invoke();
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
    public void TriggerDialogue()
    {
        MainDialogue.TriggerDialogue();
    }

    public void GetDialogue()
    {
        dialogue.Clear();
        string currentLanguage = FindObjectOfType<LanguageChanger>().currentLanguage;
        int fileIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.LogWarning(fileIndex);
        int dialogueIndex = 0;
        List<string> sentences = new List<string>();
        //File = FindObjectOfType<LanguageChanger>().Dialogue[fileIndex].levelDialogue;
        foreach (string row in FindObjectOfType<LanguageChanger>().Dialogue[fileIndex].levelDialogue)
        {
            if (row[0] == '~')
            {
                if (dialogue.Count != 0)
                {
                    dialogue[dialogueIndex-1].sentences = sentences.ToArray();
                    sentences.Clear();
                    Debug.LogError(sentences);
                    if (dialogue[dialogueIndex - 1].EventHint == null)
                    {
                        dialogue[dialogueIndex - 1].onEnd = triggers[0].trigger;
                    }
                }
                dialogue.Add(new Dialogue());
                dialogueIndex++;
                dialogue[dialogueIndex - 1].name = row.Substring(1);
                continue;
            }
            if (row.Substring(0,2) == "/~")
            {
                dialogue[dialogueIndex - 1].photo = row.Substring(2);
                continue;
            }
            if (row[0] == '&')
            {
                dialogue[dialogueIndex - 1].EventHint = row.Substring(1);
                foreach (Triggers trigger in triggers)
                {
                    if (dialogue[dialogueIndex - 1].EventHint == null)
                    {
                        dialogue[dialogueIndex - 1].onEnd = triggers[0].trigger;
                        break;
                    }
                    if (dialogue[dialogueIndex - 1].EventHint == trigger.triggerHint)
                    {
                        dialogue[dialogueIndex - 1].onEnd = trigger.trigger;
                        break;
                    }
                }
                continue;
            }
            sentences.Add(row);
        }
        MainDialogue.dialogue = dialogue.ToArray();
    }
}
