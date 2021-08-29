using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoToBed : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    public Animator Transitionanimator;
    public Collider2D Trigger;
    public UnityEvent Next;

    private void Awake()
    {
        if (Next == null)
        {
            Next = new UnityEvent();
        }
    }
    public void GoingToBed()
    {
        movement.x = -1;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("VelocityHorizontal", Mathf.Abs(movement.x * moveSpeed));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        movement.x = 0;
        StartCoroutine(LoadTransition());
    }

    IEnumerator LoadTransition()
    {
        Transitionanimator.SetBool("isEnding", true);
        yield return new WaitForSeconds(1);
        Next.Invoke();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
