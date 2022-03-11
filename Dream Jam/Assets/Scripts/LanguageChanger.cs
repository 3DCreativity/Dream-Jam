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
    public List<List<string>> Dialogue = new List<List<string>>();
    public List<string> Errors = new List<string>();
    public List<string> Names = new List<string>();
    List<string> Level = new List<string>();
    List<string> file = new List<string>();

    private void Awake()
    {
        dir = "C:/Users/User/Desktop/Content/Language Packs/";
        languages = File.ReadAllLines(dir + "languages.pack").ToList();
        dir += currentLanguage + ".language";
        file = File.ReadAllLines(dir).ToList();
        bool inUI = false;
        bool inErrors = false;
        bool inNames = false;
        bool inDialogue = false;
        bool inLevel = false;
        foreach (string line in file)
        {
            if (line[0] == '#' || line == "")
            {
                continue;
            }
            if (line[0] == '^')
            {
                string usage = line.Substring(1);
                if (inDialogue)
                {
                    inLevel = true;
                    continue;
                }
                if (usage == "UI")
                {
                    inUI = true;
                    continue;
                }
                if (usage == "Warnings/Errors")
                {
                    inErrors = true;
                    continue;
                }
                if (usage == "Names")
                {
                    inNames = true;
                    continue;
                }
            }
            if (line == "#Start Dialogue")
            {
                inDialogue = true;
                continue;
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
            if (inNames)
            {
                Names.Add(line);
                continue;
            }
            if (inLevel)
            {
                Level.Add(line);
                continue;
            }
            if (line == "/^")
            {
                inLevel = false;
                inUI = false;
                continue;
            }
        }
    }
    //Number Of Not Levels = 2
    //int numberOfLevels = SceneManager.sceneCountInBuildSettings - 2;
    public void StartupContent()
    {
         
    }
}
