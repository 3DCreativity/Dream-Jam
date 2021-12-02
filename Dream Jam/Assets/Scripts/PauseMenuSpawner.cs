using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenu;
    //public GameObject Menu1;
    //public GameObject Menu2;
    //int num;
    //bool spawned = false;
    bool paused = false;
    private DialogueInput Input;

    private void Awake()
    {
        Input = new DialogueInput();
        Input.UI.Enable();
        Input.UI.Pause.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (paused == false)
            {
                Pausing();
                paused = true;
            }
            else
            {
                Resuming();
                paused = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //num = Random.Range(1, 3);
        //var keyboard = Keyboard.current.escapeKey;
        //var gamepad = Gamepad.current.startButton;
        //if (gamepad == null)
        //{
        //    gamepad = keyboard;
        //}
        //if (keyboard.wasPressedThisFrame || gamepad.wasPressedThisFrame)
        //{
        //    if (paused == false)
        //    {
        //        Pausing();
        //        paused = true;
        //    }
        //    else
        //    {
        //        Resuming();
        //        paused = false;
        //    }
        //}
        //void PauseGame()
        //{
        //    Time.timeScale = 0f;

        //    if (spawned == false)
        //    {
        //        if (num == 1)
        //        {
        //            Instantiate(Menu1, transform);
        //        }
        //        if (num == 2)
        //        {
        //            Instantiate(Menu2, transform);
        //        }
        //        spawned = true;
        //    }
        //}
        //void ResumeGame()
        //{
        //    GameObject.Destroy(GameObject.Find("PauseMenu(Clone)"), 0f);
        //    Time.timeScale = 1f;
        //}
        
        
    }
    void Pausing()
    {
        Time.timeScale = 0f;
        FindObjectOfType<PlayerMovement>().enabled = false;
        PauseMenu.SetActive(true);
    }
    public void Resuming()
    {
        Time.timeScale = 1f;
        FindObjectOfType<PlayerMovement>().enabled = true;
        PauseMenu.SetActive(false);
    }
}
