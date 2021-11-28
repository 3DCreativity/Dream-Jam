using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Continue : MonoBehaviour
{
    public UnityEvent Conversation;

    private void Awake()
    {
        if (Conversation == null)
        {
            Conversation = new UnityEvent();
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current.enterKey;
        var gamepad = Gamepad.current.aButton;
        if (gamepad == null)
        {
            gamepad = keyboard;
        }
        if (keyboard.wasPressedThisFrame || gamepad.wasPressedThisFrame)
        {
            Conversation.Invoke();
        }
    }

    public IEnumerator AutomatedConversation(float time)
    {
        yield return new WaitForSeconds(time);
        Conversation.Invoke();
    }
}
