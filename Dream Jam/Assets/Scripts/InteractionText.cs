using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractionText : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    string lang;
    private void Awake()
    {
        text.material = new Material(text.material);
        text.material.SetFloat("_Intensity", 0.1f);
    }
    public void Initialize()
    {
        text.gameObject.SetActive(true);
        StartCoroutine(DisplayText());
    }
    IEnumerator DisplayText()
    {
        string sentence = text.text;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(.1f);
            yield return null;
        }
    }
    public void Hide()
    {
        text.gameObject.SetActive(false);
    }
    private void Update()
    {
        lang = GameObject.FindObjectOfType<GameManager>().Language;
        if (lang == "English")
        {
            text.text = "Interact";
        }
        else
        {
            text.text = "Взаимодействие";
        }
    }
}
