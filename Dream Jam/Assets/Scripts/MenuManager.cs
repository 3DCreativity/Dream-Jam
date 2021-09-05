using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ControllsMenu;
    public GameObject CreditsMenu;
    public Animator animator;
    public UnityEvent StartConversation;

    private void Awake()
    {
        if (StartConversation == null)
        {
            StartConversation = new UnityEvent();
        }
    }

    public void DisplayControllsMenu()
    {
        MainMenu.SetActive(false);
        ControllsMenu.SetActive(true);
    }
    public void DisplayCreditsMenu()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ReturnController()
    {
        ControllsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ReturnCredits()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ActionStart()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        animator.SetTrigger("isPlaying");
        yield return new WaitForSeconds(1);
        MainMenu.SetActive(false);
        StartConversation.Invoke();
    }

}
