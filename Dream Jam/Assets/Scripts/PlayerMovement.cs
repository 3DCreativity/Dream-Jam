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
        midair = animator.GetBool("isJumping");
    }
    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalVelocity", Mathf.Abs(rb.velocity.y));
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
        if (rb.velocity.y >0.5f)
        {
            animator.SetBool("isJumping", true);
        }
        if (rb.velocity.y == 2.01f && midair == true)
        {
            animator.SetBool("Jumped", true);
        }
    }
    // Update is called once per frame
    void Update()
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
