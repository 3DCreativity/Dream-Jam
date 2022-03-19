using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class LanguageChanger : MonoBehaviour
{
    
    public List<string> languages = new List<string>();
    public string currentLanguage = "English";
    public string dir;
    public List<string> UIElements = new List<string>();
    public List<LevelDialogue> Dialogue = new List<LevelDialogue>();
    public List<string> Errors = new List<string>();
    //List<string> Level = new List<string>();
    List<string> file = new List<string>();

    private void Awake()
    {
        GetLanguages();
        ChangeLanguagePrefab();
    }
    public void GetLanguages()
    {
        dir = "C:/Users/User/Desktop/Content/LanguagePacks/";
        languages = File.ReadAllLines(dir + "languages.pack").ToList();
    }
    
    public void ChangeLanguagePrefab()
    {
        string fileDir;
        fileDir = dir + currentLanguage + ".language";
        UIElements.Clear();
        Dialogue.Clear();
        Errors.Clear();
        //Level.Clear();
        file.Clear();
        file = File.ReadAllLines(fileDir).ToList();
        bool inUI = false;
        bool inErrors = false;
        bool inDialogue = false;
        bool inLevel = false;
        int levelCount = 0;
        LevelDialogue temp = new LevelDialogue();
        foreach (string line in file)
        {
            if (line == "/^")
            {
                inLevel = false;
                inUI = false;
                inErrors = false;
                continue;
            }
            //Debug.Log(line);
            if (line == "")
            {
                //Debug.Log("continue");
                continue;
            }
            if (line == "#Start Dialogue")
            {
                //Debug.Log("The dialogue part started");
                inDialogue = true;
                continue;
            }
            if (line[0] == '#')
            {
                //Debug.Log("comment");
                continue;
            }
            if (line[0] == '^')
            {
                //Debug.Log("I wrote this down");
                string usage = line.Substring(1);
                if (inDialogue)
                {
                    //Debug.Log("It's a level");
                    inLevel = true;
                    Dialogue.Add(new LevelDialogue());
                    levelCount++;
                    continue;
                }
                if (usage == "Warnings/Errors")
                {
                    //Debug.Log("It's the Errors");
                    inErrors = true;
                    continue;
                }
                if (usage == "UI")
                {
                    //Debug.Log("It's the UI");
                    inUI = true;
                    continue;
                }
            }
            if (inUI)
            {
                UIElements.Add(line);
                continue;
            }
            if (inErrors)
            {
                Errors.Add(line);
                continue;
            }
            if (inLevel)
            {
                //Debug.LogWarning("Added line to Level");
                Dialogue[levelCount-1].levelDialogue.Add(line);
                continue;
            }
            //Debug.Log("Way out of line");
        }
    }
}
