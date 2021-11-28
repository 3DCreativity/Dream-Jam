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
    public int CurrentDialogue = 0;
    private Queue<string> sentences;
    UnityEvent onEnd;
    string Developer;
    bool startedDialogue = false;
    [SerializeField]
    string Language;
    [SerializeField]
    DialogueTrigger English;
    [SerializeField]
    DialogueTrigger Bulgarian;

    private void Awake()
    {
        Language = GameObject.FindObjectOfType<GameManager>().Language;
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
    private void Update()
    {
        Language = GameObject.FindObjectOfType<GameManager>().Language;
        if (Language == "English" && startedDialogue == false && English.enabled == false) {
            Bulgarian.enabled = false;
            English.enabled = true;
        } 
        else if (Language == "Български" && startedDialogue == false && Bulgarian.enabled == false)
        {
            English.enabled = false;
            Bulgarian.enabled = true;
        }
    }
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
        if (Language == "Български")
        {
            Bulgarian.TriggerDialogue();
        }
        if (Language == "English")
        {
            English.TriggerDialogue();
        }
    }
}
