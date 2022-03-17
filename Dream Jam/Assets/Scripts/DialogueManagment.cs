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
    [SerializeField]
    private List<string> File;
    [SerializeField]
    private List<Dialogue> dialogue;
    [SerializeField]
    DialogueTrigger MainDialogue;
    [SerializeField]
    bool readFromFile = true;

    private void Awake()
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
        Dialogue temp = new Dialogue();
        List<string> sentences = new List<string>();
        File = FindObjectOfType<LanguageChanger>().Dialogue[fileIndex].levelDialogue;
        foreach (string row in File)
        {
            if (row[0] == '~')
            {
                temp.sentences = sentences.ToArray();
                sentences.Clear();
                dialogue.Add(temp);
                temp.name = row.Substring(1);
                continue;
            }
            if (row.Substring(0,2) == "/~")
            {
                temp.photo = row.Substring(2);
                continue;
            }
            if (row[0] == '&')
            {
                temp.EventHint = row.Substring(1);
                continue;
            }
            sentences.Add(row);
        }
        MainDialogue.dialogue = dialogue.ToArray();
        dialogue.Clear();

    }
}
