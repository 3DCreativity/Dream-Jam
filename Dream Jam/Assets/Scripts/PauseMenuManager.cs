using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public Animator animator;
    [SerializeField]
    public string Language;
    bool Bulgarian = false;
    [SerializeField]
    GameObject SettingsTitle;
    private List<string> file = new List<string>();

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
                    if (text == "Resume")
                    {
                        text = "��������";
                    }
                    else if (text == "Restart")
                        text = "�������";
                    else if (text == "Settings")
                        text = "���������";
                    else if (text == "Exit")
                        text = "�����";
                    else if (text == "Exit Game")
                        text = "����� ����";
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
                        text = "������";
                    else if (text == "Config")
                        text = "������.";
                    else if (text == "Back")
                        text = "�����";
                    TMP.fontSize = 30;
                    TMP.text = text;
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
                    if (text == "��������")
                    {
                        text = "Resume";
                    }
                    else if (text == "�������")
                        text = "Restart";
                    else if (text == "���������")
                        text = "Settings";
                    else if (text == "�����")
                        text = "Exit";
                    else if (text == "����� ����")
                        text = "Exit Game";
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
                    else if (text == "������")
                        text = "Music";
                    else if (text == "������.")
                        text = "Config";
                    else if (text == "�����")
                        text = "Back";
                    TMP.fontSize = 38;
                    TMP.text = text;
                }
            }


        }
    }
    public void ChangeLanguage()
    {
        if (Language == "���������")
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
    public void RestartLevel()
    {
        GameObject.FindObjectOfType<LevelLoader>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
        GameObject.FindObjectOfType<PauseMenuSpawner>().Resuming();
    }
    public void BackToMain()
    {
        GameObject.FindObjectOfType<LevelLoader>().LoadLevel(0);
        GameObject.FindObjectOfType<PauseMenuSpawner>().Resuming();
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        GameObject.FindObjectOfType<PauseMenuSpawner>().Resuming();
    }
    public void ReturnController()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    
    public void ResumeGame()
    {
        
        GameObject.FindObjectOfType<PauseMenuSpawner>().Resuming();
    }

    IEnumerator StartAnimation()
    {
        animator.SetTrigger("isPlaying");
        yield return new WaitForSeconds(1);
        MainMenu.SetActive(false);
        GameObject.FindObjectOfType<DialogueManagment>().TriggerDialogue();
    }
}
