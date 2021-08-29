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
    float horizontalMove = 0f;
    public Transform player;
    public float attackRate = 2f;
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
        animator.SetTrigger("Attack");
        player.GetComponent<PlayerMovement>().Damage(15);
        
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
            if (searchCenter.position.x - player.position.x > 0) 
                horizontalMove = -speed;
            else
                horizontalMove = speed;
        }    
        else if (Vector2.Distance(searchCenter.position, player.position)<attackRadius)
        {
            if (Time.time >= nextTimeLimit)
            {
                DamagePlayer();
                nextTimeLimit = Time.time + 1f / attackRate;
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
