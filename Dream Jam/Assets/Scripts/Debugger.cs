using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class Debugger : MonoBehaviour
{
    //Integrate Content Check
    public List<string> Log;
    List<string> UnityLogs = new List<string>();
    public int criticalErrors = 0;

    //Startup Check
    private void Awake()
    {
        Application.logMessageReceived += UnityLog;
        CheckAllSystems();
        FindObjectOfType<LanguageChanger>().GetLanguages();
        FindObjectOfType<LanguageChanger>().ChangeLanguagePrefab();
    }
    
    void UnityLog(string logString, string stackTrace, LogType type)
    {
        UnityLogs.Add(DateTime.Now.ToString("HH:mm:ss") + "/" + type + "/ " + logString);
        if (type == LogType.Assert)
        {
            criticalErrors++;
        }
    }
    //Integrate Backup Check
    //Integrate All Files Check
    void CheckAllSystems()
    {
        FindObjectOfType<LanguageChecker>().CheckLanguagePack();
    }
    private void OnApplicationQuit()
    {
        WriteLog();
    }
    void WriteLog()
    {
        string dir;
        //Decide if this should be a normal or an error log
        if (criticalErrors == 0)
        {
            dir = "C:/Users/User/Desktop/Logs/";
        }
        else
        {
            dir = "C:/Users/User/Desktop/Error_Logs/";
        }
        //Check if the directory exists
        //If not - create it
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        //See what was the last log at the directory
        int lastLog = 0;
        string[] files = Directory.GetFiles(dir);
        if (files.Length != 0)
        {
            foreach (string file in files)
            {
                Debug.Log(file);
                string fileName = Path.GetFileName(file);
                int logNumberLength = fileName.IndexOf(".") - 4;
                string logNumber = fileName.Substring(4, logNumberLength);
                Debug.Log(logNumber);
                lastLog = Convert.ToInt32(logNumber);
            }
            lastLog++;
        }
        File.WriteAllText(dir + $"log_{lastLog}.txt", $"Game Log\nDate:{DateTime.Now}\n\n");
        //Write all errors if there are any
        File.AppendAllText(dir + $"log_{lastLog}.txt", "Language Packs\n\n");
        if (Log.Count + UnityLogs.Count == 0)
        {
            File.AppendAllText(dir + $"log_{lastLog}.txt", "No Logs\n\n");
            return;
        }
        foreach (string log in Log)
        {
            File.AppendAllText(dir + $"log_{lastLog}.txt", log + "\n");
        }
        File.AppendAllText(dir + $"log_{lastLog}.txt", "\nUnity Logs\n\n");
        foreach (string log in UnityLogs)
        {
            File.AppendAllText(dir + $"log_{lastLog}.txt", log + "\n");
        }

    }
}
