using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using TMPro;
using UnityEngine.Audio;
using System.IO;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    Dictionary<Guid, string> overrides;
    //Menus
    [SerializeField]
    GameObject Gameplay;
    [SerializeField]
    GameObject Graphics;
    [SerializeField]
    GameObject Music;
    [SerializeField]
    GameObject MainSettings;
    [SerializeField]
    GameObject PS4;
    [SerializeField]
    GameObject XBOX;
    //Keyboard Configuration menu
    [SerializeField]
    GameObject Key;
    //Controller listening menu
    [SerializeField]
    GameObject Listener;

    [SerializeField]
    GameObject languages;

    Resolution[] resolutions;
    public Dropdown resolutionsDropdown;
    public Dropdown languageDropdown;
    public AudioMixer audioMixer;
    private void Awake()
    {
        overrides = new Dictionary<Guid, string>();
        //Finding all required game objects
        //Gameplay = GameObject.Find("Gameplay");
        //Graphics = GameObject.Find("Graphics");
        //Music = GameObject.Find("Music");
        //MainSettings = GameObject.Find("MainSettings");
        //PS4 = GameObject.Find("PS4");
        //XBOX = GameObject.Find("XBOX");
        //Key = GameObject.Find("Keyboard/Mouse");
    }
    public void SaveUserRebinds(InputActionAsset asset)
    {
        
        foreach (var map in asset.actionMaps)
            foreach (var binding in map.bindings)
            {
                if (!string.IsNullOrEmpty(binding.overridePath))
                    overrides[binding.id] = binding.overridePath;
            }
    }

    public void LoadUserRebinds(InputActionAsset asset)
    {
        foreach (var map in asset.actionMaps)
        {
            var bindings = map.bindings;
            for (var i = 0; i < bindings.Count; ++i)
            {
                if (overrides.TryGetValue(bindings[i].id, out var overridePath))
                    map.ApplyBindingOverride(i, new InputBinding { overridePath = overridePath });                                    
            }
        }
    }


    
    public void GameplayBtnPressed()
    {
        if (MainSettings.activeInHierarchy && Gameplay.activeInHierarchy)
        {
            return;
        }
        else
        {
            //Deactivating other setting game objects
            PS4.SetActive(false);
            XBOX.SetActive(false);
            Key.SetActive(false);
            Graphics.SetActive(false);
            Listener.SetActive(false);
            Music.SetActive(false);
            //Activating Required ones
            Gameplay.SetActive(true);
            MainSettings.SetActive(true);
        }
    }

    
    public void GraphicsBtnPressed()
    {
        if (Graphics.activeInHierarchy)
        {
            return;
        }
        else
        {
            //Deactivating other setting game objects
            Gameplay.SetActive(false);
            Listener.SetActive(false);
            Music.SetActive(false);
            //Activating Required ones
            Graphics.SetActive(true);
        }
    }
    
    public void MusicBtnPressed()
    {
        if (Music.activeInHierarchy)
        {
            return;
        }
        else
        {
            //Deactivating other setting game objects
            Gameplay.SetActive(false);
            Listener.SetActive(false);
            Graphics.SetActive(false);
            //Activating Required ones
            Music.SetActive(true);
        }
    }
    
    public void MusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolumeParam", volume);
    }
    public void Master(float volume)
    {
        audioMixer.SetFloat("MasterVolumeParam", volume);
    }
    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolumeParam", volume);
    }
    public void Brightness(float level)
    {
        Color original = GameObject.Find("BrightnessControl").GetComponent<Image>().color;
        GameObject.Find("BrightnessControl").GetComponent<Image>().color = new Color(original.r,original.g,original.b,level);
    }
    public void Fullscreen(bool full)
    {
        Screen.fullScreen = full;
    }
    public void Bloom(bool bloom)
    {
        GameObject.FindObjectOfType<GameManager>().Bloom = bloom;
    }
    public void Resolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i=0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        languageDropdown.ClearOptions();
        string readFromFilePath = Application.streamingAssetsPath + "Languages" + ".txt";
        List<string> languages = File.ReadAllLines(readFromFilePath).ToList();
        languageDropdown.AddOptions(languages);

    }
    
}
