using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load the loading scene
        AsyncOperation loadingSceneOperation = SceneManager.LoadSceneAsync("Loading");
        while (!loadingSceneOperation.isDone)
        {
            yield return null;
        }

        // Load the target scene
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
        sceneLoadOperation.allowSceneActivation = false;

        // Update loading progress
        while (!sceneLoadOperation.isDone)
        {
            float progress = Mathf.Clamp01(sceneLoadOperation.progress / 0.9f);
            // Update your loading UI here
            // For example: loadingBar.fillAmount = progress;

            if (sceneLoadOperation.progress >= 0.9f)
            {
                // Finish loading
                sceneLoadOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}