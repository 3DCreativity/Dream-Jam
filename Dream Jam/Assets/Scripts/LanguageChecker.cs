using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
//Future update: Use try/catch method
public class LanguageChecker : MonoBehaviour
{
    public List<string> languages = new List<string>();
    List<string> logs = new List<string>();
    int criticalErrors = 0;
    bool backupOnline = false;
    public void CheckLanguagePack()
    {
        string languageDir = "C:/Users/User/Desktop/Content/LanguagePacks/";
        string languageBackupDir = "C:/Users/User/Desktop/Backup/LanguagePacks/";
        backupOnline = CheckBackup(languageDir, languageBackupDir);
        Check_RepairFiles(languageDir, languageBackupDir);
        //if (criticalErrors == 0) 
        //{
        //    Check_RepairContent(languageDir, languageBackupDir);
        //}
        //Register any logs and errors from the check
        foreach (string log in logs)
        {
            FindObjectOfType<Debugger>().Log.Add(log);
            FindObjectOfType<Debugger>().criticalErrors += criticalErrors;
        }
        
    }

    bool CheckBackup(string languageDir, string languageBackupDir)
    {
        //Check if a backup is present
        //Create one if not and return
        if (!Directory.Exists(languageBackupDir))
        {
            logs.Add("Warning: Lnaguage Pack: No Backup Directory Found");
            Directory.CreateDirectory(languageBackupDir);
            if (Directory.Exists(languageBackupDir))
                logs.Add("Log: Language Pack: Empty Backup Path Created. Content Check Underway...");
            else
                logs.Add("Error: Language Pack: Backup creation unsuccessful");
            return false;
        }
        //Check if all files are intact
        //If not - return
        string[] files = Directory.GetFiles(languageDir);
        foreach (string file in files)
        {
            if (!File.Exists(languageBackupDir + file))
            {
                return false;
            }
        }
        return true;
    }

    void Check_RepairFiles(string languageDir, string languageBackupDir)
    {
        //Check if the directory is present
        //Create if not
        if (!Directory.Exists(languageDir))
        {
            Directory.CreateDirectory(languageDir);
            //If a backup is present all will be backed up
            if (backupOnline)
            {
                foreach (string file in Directory.GetFiles(languageBackupDir))
                {
                    File.Copy(languageBackupDir + file, languageDir + file);
                }
            }
        }
        //Check if all files are intact
        //Copy them if not
        string[] files = Directory.GetFiles(languageDir);
        //Search for a language pack file
        //if not found - copy it
        bool foundLanguagePacks = false;
        foreach (string file in files)
        {
            if (file == languageDir + "languages.pack")
            {
                foundLanguagePacks = true;
                break;
            }
        }
        if (!foundLanguagePacks)
        {
            if (backupOnline)
            {
                File.Copy(languageBackupDir + "languages.pack", languageDir + "languages.pack");
            }
            else
            {
                logs.Add("Warning: Language Pack: No pack file found. Creating from template...");
                File.WriteAllText(languageDir + "languages.pack", "English\nБългарски");
                if (File.Exists(languageDir + "languages.pack"))
                {
                    logs.Add("Log: Language Pack: Creation successful\n");
                }
                else
                {
                    logs.Add("Error: Language Pack: Creation unsuccessful. Reinstallation may be required.\n");
                    criticalErrors++;
                }
                
            }
        }
        //Getting all languages from the pack
        languages = File.ReadAllLines(languageDir + "languages.pack").ToList();
        //Find if all files are listed and present
        //If not listed - add them; if missing - add an error
        int languagesDetected = 0;
        foreach (string file in files)
        {
            bool foundLanguage = false;
            foreach (string language in languages)
            {
                if (file == languageDir + language + ".language")
                {
                    //Debug.Log(file + "\n" + language + "\n\n");
                    foundLanguage = true;
                    languagesDetected++;
                    break;
                }

            }
            Debug.Log(file + "\n");
            if (!foundLanguage)
            {
                if (file != languageDir + "languages.pack")
                {
                    languages.Add($"\n{Path.GetFileName(languageDir + file)}");
                }
            }
        }
        if (languagesDetected < languages.Count())
        {
            foreach (string language in languages)
            {
                foreach (string file in files)
                {
                    if (file == language + ".language")
                    {
                        break;
                    }
                    if (backupOnline)
                    {
                        File.Copy(languageBackupDir + language + ".language", languageDir + language + ".language");
                        break;
                    }
                    logs.Add($"Error: Language Packs: {language}.language is missing! Reinstallation may be required.\n");
                    criticalErrors++;
                }
            }
        }
    }
    //void Check_RepairContent(string languageDir,string languageBackupDir)
    //{
    //    //See if there is backup and compare from there
    //    //If there is not it must be freshly installed. Should be fine.(I could add an easter egg or a puzzle for this case)
    //    if (!backupOnline)
    //    {
    //        return;
    //    }
    //    //string[] langFiles = Directory.GetFiles(languageDir);
    //    FindObjectOfType<LanguageChanger>().GetLanguages();
    //    FindObjectOfType<LanguageChanger>().ChangeLanguagePrefab();
    //    //foreach (string file in langFiles)
    //    //{
    //    //    //Check the contents

    //    //}
    //}
    //private void GenerateLanguagePackBackup(string languageDir, string languageBackupDir)
    //{
    //    //Scan all files in the Language Pack Directory and copy them into the Backup Directory
    //    string[] files = Directory.GetFiles(languageDir);
    //    foreach (string file in files)
    //    {
    //        File.Copy(file, file.Replace(languageDir,languageBackupDir), true);
    //    }
    //}
}
