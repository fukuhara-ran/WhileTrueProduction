using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public AudioSource backgroundMusic;
    public AudioSource sfxAudioSource;
    public Slider musicSlider;
    public Slider sfxSlider;
    public float volumeStep = 0.1f;

    void Start()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenuUI.activeSelf)
            {
                CloseOptions();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenOptions()
    {
        optionsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void SetMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
        musicSlider.value = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        sfxSlider.value = volume;
    }

    public void IncreaseMusicVolume()
    {
        SetMusicVolume(Mathf.Clamp(backgroundMusic.volume + volumeStep, 0f, 1f));
    }

    public void DecreaseMusicVolume()
    {
        SetMusicVolume(Mathf.Clamp(backgroundMusic.volume - volumeStep, 0f, 1f));
    }

    public void IncreaseSFXVolume()
    {
        SetSFXVolume(Mathf.Clamp(sfxAudioSource.volume + volumeStep, 0f, 1f));
    }

    public void DecreaseSFXVolume()
    {
        SetSFXVolume(Mathf.Clamp(sfxAudioSource.volume - volumeStep, 0f, 1f));
    }
}
