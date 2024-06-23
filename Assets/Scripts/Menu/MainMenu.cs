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
    public Image musicVolumeImage;
    public Image sfxVolumeImage;
    public Sprite[] volumeSprites;
    public float volumeStep = 11f;

    void Start()
    {
        LoadVolumeSettings();

        UpdateVolumeImage(backgroundMusic.volume * 100f, musicVolumeImage);
        UpdateVolumeImage(sfxAudioSource.volume * 100f, sfxVolumeImage);

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
        SceneTransitionManager.Instance.LoadScene("Load");
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
