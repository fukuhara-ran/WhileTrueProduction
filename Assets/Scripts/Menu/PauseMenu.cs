using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    private bool isPaused = false;
    public float fadeDuration = 0.5f;
    public AudioSource backgroundMusic;
    public AudioSource sfxAudioSource;
    public Image musicVolumeImage;
    public Image sfxVolumeImage;
    public Sprite[] volumeSprites;
    public float volumeStep = 11f;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);

        UpdateVolumeImage(backgroundMusic.volume * 100f, musicVolumeImage);
        UpdateVolumeImage(sfxAudioSource.volume * 100f, sfxVolumeImage);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionMenuUI.activeSelf)
            {
                CloseOptions();
            }
            else if (isPaused)
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

    public void OpenOptions()
    {
        optionMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void CloseOptions()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
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

    public void IncreaseMusicVolume()
    {
        float newVolume = Mathf.Clamp(backgroundMusic.volume * 100f + volumeStep, 0f, 100f);
        SetMusicVolume(newVolume);
    }

    public void DecreaseMusicVolume()
    {
        float newVolume = Mathf.Clamp(backgroundMusic.volume * 100f - volumeStep, 0f, 100f);
        SetMusicVolume(newVolume);
    }

    public void IncreaseSFXVolume()
    {
        float newVolume = Mathf.Clamp(sfxAudioSource.volume * 100f + volumeStep, 0f, 100f);
        SetSFXVolume(newVolume);
    }

    public void DecreaseSFXVolume()
    {
        float newVolume = Mathf.Clamp(sfxAudioSource.volume * 100f - volumeStep, 0f, 100f);
        SetSFXVolume(newVolume);
    }

    void SetMusicVolume(float volume)
    {
        backgroundMusic.volume = volume / 100f;
        UpdateVolumeImage(volume, musicVolumeImage);
        SaveVolumeSettings();
    }

    void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume / 100f;
        UpdateVolumeImage(volume, sfxVolumeImage);
        SaveVolumeSettings();
    }

    void UpdateVolumeImage(float volume, Image volumeImage)
    {
        int index = Mathf.Clamp(Mathf.RoundToInt(volume / 11f), 0, 10);
        volumeImage.sprite = volumeSprites[index];
    }

    void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", backgroundMusic.volume);
        PlayerPrefs.SetFloat("SFXVolume", sfxAudioSource.volume);
        PlayerPrefs.Save();
    }

    void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            backgroundMusic.volume = 1f; // Default volume
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxAudioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            sfxAudioSource.volume = 1f; // Default volume
        }
    }
}
