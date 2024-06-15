using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public AudioSource backgroundMusic;
    public float fadeDuration = 0.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeOutAndLoadScene(SceneManager.GetActiveScene().name));
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeOutAndLoadScene("Main Menu"));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float startVolume = backgroundMusic.volume;

        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            backgroundMusic.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        backgroundMusic.volume = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
