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

    private void Awake()
    {
        if (onEnd == null)
        {
            onEnd = new UnityEvent();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue, UnityEvent onEnding)
    {
        animator.SetBool("isOpen", true);

        if (dialogue.name == "Magician")
        {
            Character.sprite = faces[1];
        }
        else
        {
            Character.sprite = faces[0];
        }

        nameText.text = dialogue.name;

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
