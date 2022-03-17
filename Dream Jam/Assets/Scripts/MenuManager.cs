using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;
    public Animator animator;
    public string Language;
    //[SerializeField]
    //bool LanguageChanged = false;
    [SerializeField]
    [TextArea(10,20)]
    string Credits_Text;
    [SerializeField]
    GameObject SettingsTitle;
    [SerializeField]
    GameObject CreditsTitle;
    [SerializeField]
    Text Credits;
    private List<string> file = new List<string>();
        
    private void Awake()
    {
        ChangeLanguage();
    }
    public void ChangeLanguage()
    {
        file = FindObjectOfType<LanguageChanger>().UIElements;
        List<string> names = new List<string>();
        List<string> titles = new List<string>();
        bool inNames = false;
        bool inTitles = false;
        bool inCredits = false;
        Credits_Text = "";
        foreach (string line in file)
        {
            if (line[0] == '~')
            {
                string temp = line.Substring(1);
                if (temp == "Titles")
                {
                    inTitles = true;
                    continue;
                }
                if (temp == "PS4" || temp == "XBOX" || temp == "Keyboard/Mouse" || temp == "Names")
                {
                    inNames = true;
                    continue;
                }
                if (temp == "Credits")
                {
                    inCredits = true;
                }
            }
            if (inNames)
            {
                names.Add(line);
            }
            if (inTitles)
            {
                titles.Add(line);
            }
            if (inCredits)
            {
                Credits_Text += line + "\n";
            }
            if (line.Substring(0, 2) == "/~")
            {
                inNames = false;
                inTitles = false;
            }
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("LanguageDependent"))
        {
            if (obj.name == "Title")
            {
                foreach (string title in titles)
                {
                    int length = title.IndexOf("=") - 1;
                    if (obj.transform.parent.gameObject.name + "_Title" == title.Substring(0, length))
                    {
                        if (obj.GetComponent<TMP_Text>() != null)
                        {
                            obj.GetComponent<TMP_Text>().text = name.Substring(length + 2);
                            break;
                        }
                        else
                        {
                            obj.transform.GetComponent<Text>().text = name.Substring(length + 2);
                            break;
                        }
                    }
                }
                continue;
                
            }
            foreach(string name in names)
            {
                int length = name.IndexOf("=") - 1;
                if (obj.name == name.Substring(0, length))
                {
                    if (obj.transform.GetComponentInChildren<TMP_Text>() != null)
                    {
                        obj.transform.GetComponentInChildren<TMP_Text>().text = name.Substring(length + 2);
                        break;
                    }
                    else
                    {
                        obj.transform.GetComponentInChildren<Text>().text = name.Substring(length + 2);
                        break;
                    }
                }
            }
        }
    }
    public void DisplayControllsMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void DisplayCreditsMenu()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ReturnController()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ReturnCredits()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ActionStart()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        animator.SetTrigger("isPlaying");
        yield return new WaitForSeconds(1);
        MainMenu.SetActive(false);
        GameObject.FindObjectOfType<DialogueManagment>().TriggerDialogue();
    }

}
