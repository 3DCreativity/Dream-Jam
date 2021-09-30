using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public float glitchIntensity = 0f;
    public LevelLoader levelLoader;
    public float MaxStamina = 100f;
    public float TopStamina = 100f;
    public float Stamina = 100f;
    public SpriteRenderer BG;
    public GameObject Player;
    public TilemapRenderer Effect;
    bool isGlitching = false;
    float random;
    public float glitchTimeRangeA = 3f;
    public float glitchTimeRangeB = 10f;
    float staminaRate = 1f;
    float time = 0f;
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
        onStart.Invoke();
    }
    void Update()
    {
        if (Stamina <= 80f && Stamina >= 60f)
        {
            BG.material.SetFloat("_Intensity", 0.02f);
            Effect.enabled = false;
        }
        if (Stamina < 60f && Stamina >= 40f)
        {
            Effect.enabled = true;
            Effect.material.SetFloat("_Intensity", 0.03f);
        }
        if (Stamina < 40f && isGlitching == false)
        {
            isGlitching = true;
            StartCoroutine(GlitchFloor());
        }
    }
    IEnumerator GlitchFloor()
    {
        yield return new WaitForSeconds(random);
        ActivateGlitch();
        yield return new WaitForSeconds(3);
        Player.GetComponent<CircleCollider2D>().enabled = false;
        Player.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Player.GetComponent<CircleCollider2D>().enabled = true;
        Player.GetComponent<BoxCollider2D>().enabled = true;
        random = GenerateRandomNumber(glitchTimeRangeA, glitchTimeRangeB);
        DeactivateGlitch();
        isGlitching = false;
    }
    
    void FixedUpdate()
    {
        if (Stamina < TopStamina && Time.time == time)
        {
            Stamina += 10;
            time = Time.time + 1f/staminaRate;
        }
        else if (Stamina > TopStamina)
        {
            Stamina = TopStamina;
        }
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
