using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    
    public float glitchIntensity = 0f;
    [SerializeField]
    LevelLoader levelLoader;
    int Focus;
    [SerializeField]
    SpriteRenderer BG;
    [SerializeField]
    SpriteRenderer BG2;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    TilemapRenderer Effect;
    [SerializeField]
    TilemapRenderer Collidable;
    [SerializeField]
    TilemapRenderer Additions;
    public bool GlitchEffects = true;

    bool isGlitching = false;
    float random;
    public float glitchTimeRangeA = 3f;
    public float glitchTimeRangeB = 10f;
    //float staminaRate = 1f;
    //float time = 0f;
    public UnityEvent onStart;

    float GenerateRandomNumber(float RangeA, float RangeB)
    {
        float generatedNumber = Random.Range(RangeA, RangeB);
        return generatedNumber;
    }

    void Awake()
    {
        if (onStart == null)
        {
            onStart = new UnityEvent();
        }
        random = GenerateRandomNumber(glitchTimeRangeA, glitchTimeRangeB);
        
    }
    void Start()
    {
        StartCoroutine(Starting_up());
    }
    IEnumerator Starting_up()
    {
        yield return new WaitForSeconds(.1f);
        onStart.Invoke();
    }
    void Update()
    {
        Focus = GameObject.FindObjectOfType<PlayerMovement>().focusLeft;
        Collidable.material.SetFloat("_Intensity", glitchIntensity);
        Additions.material.SetFloat("_Intensity", glitchIntensity);
        if (Focus <= 80 && Focus >= 60)
        {
            BG.material.SetFloat("_Intensity", 0.02f);
            BG2.material.SetFloat("_Intensity", 0.02f);
            Effect.enabled = false;
        }
        else if (Focus > 80)
        {
            BG.material.SetFloat("_Intensity", 0f);
            BG2.material.SetFloat("_Intensity", 0f);
        }
        if (Focus < 60 && Focus >= 40)
        {
            Effect.enabled = true;
            Effect.material.SetFloat("_Intensity", 0.03f);
        }
        else if (Focus > 60)
        {
            Effect.enabled = false;
            Effect.material.SetFloat("_Intensity", 0f);
        }
        if (Focus < 40 && isGlitching == false)
        {
            isGlitching = true;
            StartCoroutine(GlitchFloor());
        }
        else if (Focus > 40)
        {
            isGlitching = false;
            StopCoroutine(GlitchFloor());
        }
    }
    IEnumerator GlitchFloor()
    {
        yield return new WaitForSeconds(random);
        ActivateGlitch();
        yield return new WaitForSeconds(3);
        Player.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Player.GetComponent<CapsuleCollider2D>().enabled = true;
        random = GenerateRandomNumber(glitchTimeRangeA, glitchTimeRangeB);
        DeactivateGlitch();
        isGlitching = false;
    }
    
    void FixedUpdate()
    {
        //if (Stamina < TopStamina && Time.time == time)
        //{
        //    Stamina += 10;
        //    time = Time.time + 1f/staminaRate;
        //}
        //else if (Stamina > TopStamina)
        //{
        //    Stamina = TopStamina;
        //}
    }
    public void ActivateGlitch()
    {
        glitchIntensity = 0.05f;
    }
    public void DeactivateGlitch()
    {
        glitchIntensity = 0f;
    }
    public void OnDeath()
    {
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
