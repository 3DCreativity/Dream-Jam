using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerListener : MonoBehaviour
{
    Image disconnected;
    [SerializeField]
    GameObject PS4;
    [SerializeField]
    GameObject XBOX;
    [SerializeField]
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        disconnected = GetComponent<Image>();
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    break;

                case InputDeviceChange.Removed:
                    PS4.SetActive(false);
                    XBOX.SetActive(false);
                    disconnected.enabled = true;
                    text.enabled = true;
                    break;
            }
        };
    }
    private void OnEnable()
    {
        PS4.SetActive(false);
        XBOX.SetActive(false);
        disconnected.enabled = true;
        text.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            if (InputSystem.IsFirstLayoutBasedOnSecond(Gamepad.current.layout, "DualShock4GamepadHID"))
            {
                PS4.SetActive(true);
                disconnected.enabled = false;
                text.enabled = false;
            }
            else
            {
                XBOX.SetActive(true);
                disconnected.enabled = false;
                text.enabled = false;
            }
                
        }
    }
}
