using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeImage : MonoBehaviour
{
    public Sprite newImage;
    public UnityEvent ContinueConversation;

    private void Awake()
    {
        if (ContinueConversation == null)
        {
            ContinueConversation = new UnityEvent();
        }
    }
    public void changeImage()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(2);
        GetComponent<SpriteRenderer>().sprite = newImage;
        yield return new WaitForSeconds(2);
        ContinueConversation.Invoke();
    }
}
