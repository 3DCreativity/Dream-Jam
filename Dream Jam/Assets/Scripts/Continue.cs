using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        if (Input.anyKeyDown)
        {
            Conversation.Invoke();
        }
    }
}
