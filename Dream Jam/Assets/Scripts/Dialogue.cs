using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue 
{
    public string photo;
    public string name;

    [TextArea(2,10)]
    public string[] sentences;

    public string EventHint;
    public UnityEvent onEnd;
}
