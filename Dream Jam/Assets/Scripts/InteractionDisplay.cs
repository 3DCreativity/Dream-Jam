using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractionDisplay : MonoBehaviour
{
    public bool able = false;
    public UnityEvent interaction;
    private void Update()
    {
        if (interaction == null)
            interaction = new UnityEvent();
    }
    public void DisplayInteraction(string name)
    {
        Debug.Log("Interacted" + name);
        GameObject.Find(name).GetComponentInChildren<InteractionText>().Initialize();
    }
    public void FadeInteraction(string name)
    {
        Debug.Log("Exited" + name);
        GameObject.Find(name).GetComponentInChildren<InteractionText>().Hide();
    }
    public void Interact()
    {
        interaction.Invoke();
    }
}
