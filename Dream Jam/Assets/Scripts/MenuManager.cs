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
    bool Bulgarian = false;
    //[SerializeField]
    //bool LanguageChanged = false;
    [SerializeField]
    [TextArea(10,20)]
    string EnglishCredits;
    [SerializeField]
    [TextArea(10, 20)]
    string BulgarianCredits;
    [SerializeField]
    GameObject SettingsTitle;
    [SerializeField]
    GameObject CreditsTitle;
    [SerializeField]
    Text Credits;

    private void Update()
    {
        if (Bulgarian)
        {
            GameObject.FindObjectOfType<GameManager>().Language = "���������";
        }
        else
        {
            GameObject.FindObjectOfType<GameManager>().Language = "English";
        }
        Language = GameObject.FindObjectOfType<GameManager>().Language;
        if (Language == "���������")
        {
            TextMeshProUGUI TMP;
            if (MainMenu.activeInHierarchy)
            {
                var MainButtons = MainMenu.GetComponentsInChildren<Button>();
                foreach (Button button in MainButtons)
                {
                    TMP = button.GetComponentInChildren<TextMeshProUGUI>();
                    string text = TMP.text;
                    if (text == "Start")
                    {
                        text = "�����";
                    }
                    else if (text == "Settings")
                        text = "���������";
                    else if (text == "Credits")
                        text = "�������";
                    else if (text == "Exit")
                        text = "�����";
                    TMP.fontSize = 30;
                    TMP.text = text;
                }
            }
            if (SettingsMenu.activeInHierarchy)
            {
                TMP = SettingsTitle.GetComponent<TextMeshProUGUI>();
                TMP.text = "���������";
                var SettingsButtons = SettingsMenu.GetComponentsInChildren<Button>();
                foreach (Button button in SettingsButtons)
                {
                    var bannedButtons = (button.gameObject.name == "MovementBtn") || (button.gameObject.name == "InteractBtn") || (button.gameObject.name == "EnablAttack1Btn") || (button.gameObject.name == "EnablAttack2Btn") || (button.gameObject.name == "Attack1Btn") || (button.gameObject.name == "Attack2Btn") || (button.gameObject.name == "HealBtn") || (button.gameObject.name == "ShieldBtn") || (button.gameObject.name == "FocusBtn");
                    if (bannedButtons)
                        continue;
                    TMP = button.GetComponentInChildren<TextMeshProUGUI>();
                    string text = TMP.text;
                    if (text == "Gameplay")
                    {
                        text = "��������";
                    }
                    else if (text == "Graphics")
                        text = "�������";
                    else if (text == "Music")
                        text = "M�����";
                    else if (text == "Config")
                        text = "������.";
                    else if (text == "Back")
                        text = "�����";
                    TMP.fontSize = 30;
                    TMP.text = text;
                }
            }
            if (CreditsMenu)
            {
                TMP = CreditsTitle.GetComponentInChildren<TextMeshProUGUI>();
                TMP.text = "�������";
                Credits.text = BulgarianCredits;
                var CreditsButtons = CreditsMenu.GetComponentsInChildren<Button>();
                foreach (Button button in CreditsButtons)
                {
                    TMP = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (TMP != null)
                    {
                        string text = TMP.text;
                        if (text == "Back")
                            text = "�����";
                        TMP.fontSize = 30;
                        TMP.text = text;
                    }
                }
            }
            
        }
        else
        {
            
            TextMeshProUGUI TMP;
            if (MainMenu.activeInHierarchy)
            {
                var MainButtons = MainMenu.GetComponentsInChildren<Button>();
                foreach (Button button in MainButtons)
                {
                    TMP = button.GetComponentInChildren<TextMeshProUGUI>();
                    string text = TMP.text;
                    if (text == "�����")
                    {
                        text = "Start";
                    }
                    else if (text == "���������")
                        text = "Settings";
                    else if (text == "�������")
                        text = "Credits";
                    else if (text == "�����")
                        text = "Exit";
                    TMP.fontSize = 38;
                    TMP.text = text;
                }
            }
            if (SettingsMenu.activeInHierarchy)
            {
                TMP = SettingsTitle.GetComponent<TextMeshProUGUI>();
                TMP.text = "Settings";
                var SettingsButtons = SettingsMenu.GetComponentsInChildren<Button>();
                foreach (Button button in SettingsButtons)
                {                
                    var bannedButtons = (button.gameObject.name == "MovementBtn") || (button.gameObject.name == "InteractBtn") || (button.gameObject.name == "EnablAttack1Btn") || (button.gameObject.name == "EnablAttack2Btn") || (button.gameObject.name == "Attack1Btn") || (button.gameObject.name == "Attack2Btn") || (button.gameObject.name == "HealBtn") || (button.gameObject.name == "ShieldBtn") || (button.gameObject.name == "FocusBtn");
                    if (bannedButtons)
                        continue;
                    TMP = button.GetComponentInChildren<TextMeshProUGUI>();
                    string text = TMP.text;
                    if (text == "��������")
                    {
                        text = "Gameplay";
                    }
                    else if (text == "�������")
                        text = "Graphics";
                    else if (text == "M�����")
                        text = "Music";
                    else if (text == "������.")
                        text = "Config";
                    else if (text == "�����")
                        text = "Back";
                    TMP.fontSize = 38;
                    TMP.text = text;
                }
            }
            if (CreditsMenu.activeInHierarchy)
            {
                TMP = CreditsTitle.GetComponentInChildren<TextMeshProUGUI>();
                TMP.text = "Credits";
                Credits.text = EnglishCredits;
                var CreditsButtons = CreditsMenu.GetComponentsInChildren<Button>();
                foreach (Button button in CreditsButtons)
                {
                    TMP = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (TMP != null)
                    {
                        string text = TMP.text;
                        if (text == "�����")
                            text = "Back";
                        TMP.fontSize = 38;
                        TMP.text = text;
                    }
                }
            }
            
        }
    }
    public void ChangeLanguage()
    {
        if (Bulgarian)
        {
            Bulgarian = false;
        }
        else
        {
            Bulgarian = true;
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
