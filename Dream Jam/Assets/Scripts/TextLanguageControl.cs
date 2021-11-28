using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguageControl : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    [TextArea(3,10)]
    string Bulgarian;
    [SerializeField]
    [TextArea(3,10)]
    string English;
    [SerializeField]
    string Language;
    private void Awake()
    {
        Language = GameObject.FindObjectOfType<GameManager>().Language;
    }
    private void Update()
    {
        Language = GameObject.FindObjectOfType<GameManager>().Language;
        if (Language == "English")
        {
            text.text = English;
        }
        else
        {
            text.text = Bulgarian;
        }
    }
}
