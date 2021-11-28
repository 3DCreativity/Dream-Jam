using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public Text Level;
    public Text Finished;
    public Animator transitionAnimator;
    public GameObject transition;

    private void Awake()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevel(int sceneIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        Level.text = name.Substring(0, dot);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        transitionAnimator.SetBool("isEnding", true);
        yield return new WaitForSeconds(2);
        transition.SetActive(false);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";
            if (operation.progress >= .9f)
            {
                Finished.text = "Press Enter/X button to continue";
                progressText.text = "";
                if (Gamepad.current.aButton.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
