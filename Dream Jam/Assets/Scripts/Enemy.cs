using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]
    int currentHealth;
    public Animator animator;
    Material glitch;
    float glitched = 1f;
    float glitching = 0f;
    bool death = false;
    public bool drawn = false;
    [SerializeField]
    UnityEvent onDeath;
    private void Awake()
    {
        if(onDeath == null)
        {
            onDeath = new UnityEvent();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        glitch = GetComponent<SpriteRenderer>().material;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        //GetComponent<Enemy_Movement>().enabled = false;
        death = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (death)
        {
            if (glitching <= glitched)
            {
                glitching += Time.deltaTime;
                glitch.SetFloat("_Intensity", glitching);
            }
            else
            {
                onDeath.Invoke();
                Destroy(gameObject);
            }
        }

    }
}
