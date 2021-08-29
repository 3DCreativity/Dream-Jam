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
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(searchCenter.position, searchRadius, PlayerMask);
        Collider2D[] attack = Physics2D.OverlapCircleAll(searchCenter.position, attackRadius, PlayerMask);
        if (colliders.Length == 1 && attack.Length == 0)
        {
            if (gameObject.transform.position.x - colliders[0].transform.position.x < 0)
            {
                horizontalMove = -speed;
            }
            else
            {
                horizontalMove = speed;
            }
        }
        else if (colliders.Length == 1 && attack.Length == 1)
        {
            player = attack[0].gameObject;
            DamagePlayer();
        }
    }
    void DamagePlayer()
    {
        animator.SetTrigger("Attack");
        player.GetComponent<PlayerMovement>().Damage(15);
        float i = 0f;
        while (i <= 2f)
        {
            i += Time.deltaTime;
        }
        
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
        controller.Move(horizontalMove, false, false);
    }
}
