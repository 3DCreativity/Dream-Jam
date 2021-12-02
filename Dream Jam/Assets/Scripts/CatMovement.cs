using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField]
    CharacterController2D controller;
    [SerializeField]
    Animator anim;
    [SerializeField]
    string CatStop;
    [SerializeField]
    string CatDisappear;
    public float speed = 0f;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().material = new Material(gameObject.GetComponent<SpriteRenderer>().material);
    }
    public void Glitching()
    {
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Intensity", .02f);
    }
    public void DeactGlitch()
    {
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Intensity", 0f);
    }
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == CatStop)
        {
            speed = 0f;
            anim.SetBool("Walk", false);
        }
        if (collision.gameObject.name == CatDisappear)
        {
            Destroy(GameObject.Find("CatStop"));
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(speed, false, false);
    }
}
