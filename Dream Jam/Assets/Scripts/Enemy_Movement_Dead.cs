using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement_Dead : MonoBehaviour
{
    public CharacterController2D controller;
    public float speed = .5f;
    public LayerMask PlayerMask;
    public Animator animator;
    public Transform searchCenter;
    public float searchRadius;
    public float attackRadius;
    public int attackDamage = 15;
    float horizontalMove = 0f;
    public Transform player;
    [SerializeField]
    float attackRate = 5f;
    float nextTimeLimit = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DamagePlayer()
    {
        if (gameObject.name == "Dead")
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            int Rand = Random.Range(1,4);
            if (Rand == 1)
            {
                animator.SetTrigger("Attack1");
            }
            if (Rand == 2)
            {
                animator.SetTrigger("Attack2");
            }
            if (Rand == 3)
            {
                animator.SetTrigger("Attack3");
            }
        }
        player.GetComponent<PlayerMovement>().Damage(attackDamage);
        
    }
    private void OnDrawGizmosSelected()
    {
        if (searchCenter == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(searchCenter.position, searchRadius);
        Gizmos.DrawWireSphere(searchCenter.position, attackRadius);
    }
    private void FixedUpdate()
    {
        Vector2.Distance(searchCenter.position, player.position);
        Collider2D[] attack = Physics2D.OverlapCircleAll(searchCenter.position, attackRadius, PlayerMask);
        if (Vector2.Distance(searchCenter.position, player.position) > attackRadius && Vector2.Distance(searchCenter.position, player.position)<searchRadius)
        {
            if (GetComponent<Enemy>().drawn == false)
            {
                if (searchCenter.position.x - player.position.x > 0)
                    horizontalMove = -speed;
                else
                    horizontalMove = speed;
            }

            else
            {
                if (searchCenter.position.x - player.position.x > 0)
                    horizontalMove = speed/3;
                else
                    horizontalMove = -speed/3;
            }
        }    
        else if (Vector2.Distance(searchCenter.position, player.position)<attackRadius)
        {
            if (GetComponent<Enemy>().drawn == false)
            {
                if (Time.time >= nextTimeLimit)
                {
                    DamagePlayer();
                    nextTimeLimit = Time.time + 1f / attackRate;
                }
            }
        }
        else
        {
            horizontalMove = 0f;
        }
        controller.Move(horizontalMove, false, false);
        if (gameObject.name != "Dead")
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
}
