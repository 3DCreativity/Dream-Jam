using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class GlitchControl : MonoBehaviour
{
    public LevelManager manager;
    Material glitch;

    private void Awake()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            glitch = gameObject.GetComponent<SpriteRenderer>().material;
        }
        else if (gameObject.GetComponent<Image>() != null)
        {
            glitch = gameObject.GetComponent<Image>().material;
        }
        else if (gameObject.GetComponent<TMP_Text>() != null)
        {
            glitch = gameObject.GetComponent<TMP_Text>().material;
        }
        else if (gameObject.GetComponent<Text>() != null)
        {
            glitch = gameObject.GetComponent<Text>().material;
        }
        else
        {
            glitch = gameObject.GetComponent<TilemapRenderer>().material;
        }
    }

    public void Update()
    {
        glitch.SetFloat("_Intensity", manager.glitchIntensity);
    }
}
