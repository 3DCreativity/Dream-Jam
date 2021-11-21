using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class SettingsManager : MonoBehaviour
{
    Dictionary<Guid, string> overrides;
    private void Awake()
    {
        overrides = new Dictionary<Guid, string>();
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
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
