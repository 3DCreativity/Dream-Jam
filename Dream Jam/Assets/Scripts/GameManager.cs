using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string developerName;
    public string Language;
    public bool Bloom = true;
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    public void ChangeLanguage(Text Lang)
    {
        Language = Lang.text;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            UnityEngine.Rendering.Universal.UniversalAdditionalCameraData uac = Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
            uac.renderPostProcessing = Bloom;
        }
    }
}
