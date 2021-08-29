using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    Material glitch;
    bool jump = false;
    float horizontalMove;
    Rigidbody2D rb;

    public int HP = 100;
    public int HPLeft;
    public UnityEvent onDeath;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask enemyLayers;
    public float attackRange1 = 0.5f;
    public int attackDamage1 = 40;
    public float attackRange2 = 0.5f;
    public int attackDamage2 = 40;
    bool Attack1 = false;
    bool Attack2 = false;
    bool isDead = false;

    public float attackRate = 2f;
    float nextTime = 0f;

    public LevelManager levelMan;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        glitch = GetComponent<SpriteRenderer>().material;
        glitch.SetFloat("_Instance", 0f);
        HPLeft = HP;
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
    }
    private void OnTriggerEnter2D(Collider2D TriggerGlitch)
    {
        //if (TriggerGlitch.collider.name == "Player")
            FindObjectOfType<LevelManager>().GetComponent<LevelManager>().ActivateGlitch();
    }
    void Update()
    {
        FindObjectOfType<Camera>().transform.position = gameObject.transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Attack1") && Attack1 == true && Time.time>=nextTime)
        {
            ActivateAttack1();
            nextTime = Time.time + 1f / attackRate;
        }
        if (Input.GetButtonDown("Attack2") && Attack2 == true && Time.time >= nextTime)
        {
            ActivateAttack2();
            nextTime = Time.time + 1f / attackRate;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(EnableAttack1());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(EnableAttack2());
        }
        if (Mathf.Abs(rb.velocity.y) > 2f)
        {
            animator.SetBool("isJumping", true);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (HPLeft < HP)
            {
                levelMan.Stamina -= 10;
                HPLeft += 10;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
        
    }
    IEnumerator EnableAttack1()
    {
        glitch.SetFloat("_Intensity", 0.01f);
        yield return new WaitForSeconds(0.5f);
        glitch.SetFloat("_Intensity", 0f);
        if (Attack1 == true)
            Attack1 = false;
        else
            Attack1 = true;
    }
    IEnumerator EnableAttack2()
    {
        glitch.SetFloat("_Intensity", 0.01f);
        yield return new WaitForSeconds(0.5f);
        glitch.SetFloat("_Intensity", 0f);
        if (Attack2 == true)
            Attack2 = false;
        else
            Attack2 = true;
    }
    void ActivateAttack1()
    {
        animator.SetTrigger("Attack1");
        GetComponent<AudioSource>().Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (glitch.GetFloat("_Intensity") == 0f)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage1);
        }
    }
    void ActivateAttack2()
    {
        animator.SetTrigger("Attack2");
        GetComponent<AudioSource>().Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (glitch.GetFloat("_Intensity") == 0f)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage2);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint2 == null)
    //    {
    //        return;
    //    }
    //    Gizmos.DrawWireSphere(attackPoint2.position, attackRange2);
    //}

    public void Damage(int dam)
    {
        animator.SetTrigger("Hit");
        HPLeft -= dam;
        if (HPLeft <= 0 && isDead == false)
        {
            StartCoroutine(Death());
            isDead = true;
        }
    }

    IEnumerator Death()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(2);
        onDeath.Invoke();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }

    public void onLand()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("Jumped", false);
    }

}
