using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManagment : MonoBehaviour
{
    public Text mainText;
    public Text nameText;
    public Sprite[] faces;
    public Image Character;
    public Animator animator;
    private Queue<string> sentences;
    UnityEvent onEnd;
    string Developer;

    private void Awake()
    {
        if (onEnd == null)
        {
            onEnd = new UnityEvent();
        }
        Developer = GameObject.FindObjectOfType<GameManager>().developerName;
    }
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue, UnityEvent onEnding)
    {
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

        if (dialogue.photo == "Developer")
        {
            nameText.text = Developer;
        }
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
        onEnd.Invoke();
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
