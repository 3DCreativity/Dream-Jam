using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    bool jump = false;
    float horizontalMove;
    Rigidbody2D rb;
    bool midair;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Attack1"))
        {
            animator.SetBool("Attack1", true);
        }
        if (Input.GetButtonDown("Attack2"))
        {
            animator.SetBool("Attack2", true);
        }
        if (Mathf.Abs(rb.velocity.y) > 2f)
        {
            animator.SetBool("isJumping", true);
        }
        
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
