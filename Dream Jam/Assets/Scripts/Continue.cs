using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Continue : MonoBehaviour
{
    public UnityEvent Conversation;
    [SerializeField]
    Animator anim;
    private DialogueInput Input;
    private void Awake()
    {
        Input = new DialogueInput();
        Input.UI.Enable();
        Input.UI.Submit.performed += Continuing;
    }
    private void Continuing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Conversation.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isOpen") == true)
        {
            Input.UI.Enable();
        }
        else
        {
            Input.UI.Disable();
        }
    }

    public IEnumerator AutomatedConversation(float time)
    {
        yield return new WaitForSeconds(time);
        Conversation.Invoke();
    }
}
