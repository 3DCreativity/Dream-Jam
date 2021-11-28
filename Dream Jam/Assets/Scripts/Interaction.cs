using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;
    [SerializeField]
    UnityEvent interact;
    private void Awake()
    {
        if (onEnter == null)
            onEnter = new UnityEvent();
        if (onExit == null)
            onExit = new UnityEvent();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<InteractionDisplay>().able = true;
            GameObject.FindObjectOfType<InteractionDisplay>().interaction = interact;
            StartCoroutine(Entering());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<InteractionDisplay>().able = true;
            GameObject.FindObjectOfType<InteractionDisplay>().interaction = null;
            StartCoroutine(Exiting());
        }
    }
    IEnumerator Entering()
    {
        yield return new WaitForSeconds(.2f);
        onEnter.Invoke();
    }
    IEnumerator Exiting()
    {
        yield return new WaitForSeconds(.2f);
        onExit.Invoke();
    }
}
