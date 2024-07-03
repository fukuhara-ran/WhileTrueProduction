using UnityEngine;

public class ActorAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource secondaryAudioSource;
    private AudioSource globalAudioSource;

    [SerializeField] private AudioClip attackSFX;
    [SerializeField] private AudioClip attackedSFX;
    [SerializeField] private AudioClip dieSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip fallSFX;
    [SerializeField] private AudioClip droppableSFX;

    private void Start()
    {
        globalAudioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>().SfxSource;
        SetVolumes(SaveManager.GetInstance().ReadPref("SFXVolume"));
    }

    private void SetVolumes(float volume)
    {
        globalAudioSource.volume = volume;
        mainAudioSource.volume = volume;
        secondaryAudioSource.volume = volume;
    }

    public void PlayWalkAudio(bool isMoving, bool isGrounded)
    {
        walkAudioSource.enabled = isMoving && isGrounded;
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.clip = audioClip;
            mainAudioSource.Play();
        }
        else if (!secondaryAudioSource.isPlaying)
        {
            secondaryAudioSource.clip = audioClip;
            secondaryAudioSource.Play();
        }
    }

    public void PlayAudioGlobally(AudioClip audioClip)
    {
        globalAudioSource.clip = audioClip;
        globalAudioSource.Play();
    }

    public void PlayAttackSFX() => PlayAudio(attackSFX);
    public void PlayAttackedSFX() => PlayAudio(attackedSFX);
    public void PlayDieSFX() => PlayAudio(dieSFX);
    public void PlayJumpSFX() => PlayAudio(jumpSFX);
    public void PlayFallSFX() => PlayAudio(fallSFX);
    public void PlayDroppableSFX() => PlayAudioGlobally(droppableSFX);
}