using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float glitchIntensity = 0f;
    public void ActivateGlitch()
    {
        glitchIntensity = 0.05f;
    }
}
