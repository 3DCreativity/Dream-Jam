using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Tuthor : MonoBehaviour
{
    [SerializeField]
    GameObject cat;
    [SerializeField]
    GameObject cat1;
    [SerializeField]
    Image Focus_BG;
    [SerializeField]
    Image Focus_Fill;
    [SerializeField]
    Image Health_BG;
    [SerializeField]
    Image Health_Fill;
    [SerializeField]
    CinemachineVirtualCamera vcam1;
    [SerializeField]
    CinemachineVirtualCamera vcam2;
    bool overworld = false;
    //[SerializeField]
    //float time = 5f;
    public void CatComing()
    {
        if (cat != null)
        {
            cat.GetComponent<CatMovement>().speed = -3f;
            cat.GetComponent<Animator>().SetBool("Walk", true);
        }
        else
        {
            cat1.GetComponent<CatMovement>().speed = -3f;
            cat1.GetComponent<Animator>().SetBool("Walk", true);
        }
    }
    public void CatLeaving()
    {
        cat.GetComponent<CatMovement>().speed = 3f;
        cat.GetComponent<Animator>().SetBool("Walk", true);
    }
    
    public void Focus_Discover()
    {
        Focus_BG.color = new Color(Focus_BG.color.r, Focus_BG.color.b, Focus_BG.color.g, 1f);
        Focus_Fill.color = new Color(Focus_Fill.color.r, Focus_Fill.color.b, Focus_Fill.color.g, 1f);
        FindObjectOfType<PlayerMovement>().DisableGlitch();
        FindObjectOfType<LevelManager>().DeactivateGlitch();
        GameObject.FindObjectOfType<DialogueManagment>().TriggerDialogue();
    }
    public void Health_Discover()
    {
        Health_BG.color = new Color(Health_BG.color.r, Health_BG.color.b, Health_BG.color.g, 1f);
        Health_Fill.color = new Color(Health_Fill.color.r, Health_Fill.color.b, Health_Fill.color.g, 1f);
        PlayerLockDisable();
        FindObjectOfType<DialogueManagment>().TriggerDialogue();
    }
    public void PlayerLockEnable()
    {
        FindObjectOfType<PlayerMovement>().movelock = true;
        FindObjectOfType<PlayerMovement>().jumplock = true;
    }
    public void PlayerLockDisable()
    {
        FindObjectOfType<PlayerMovement>().movelock = false;
        FindObjectOfType<PlayerMovement>().jumplock = false;
    }
    public void Respawn()
    {
        Vector3 position = FindObjectOfType<PlayerMovement>().gameObject.transform.position;
        position.y = 5f;
        FindObjectOfType<PlayerMovement>().gameObject.transform.position = position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CameraSwitcher()
    {
        if (!overworld)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        overworld = !overworld;
    }
}
