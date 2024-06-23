using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField] protected Rigidbody2D rb;
    protected ContactFilter2D contactFilter2D = new();
    [SerializeField] protected Collider2D PlayerDetectorCollision;

    protected AudioSource GlobalAudioSource;
    [SerializeField] protected AudioClip CollectedSFX;

    protected void Erase() {
        PlayAudioGlobally(CollectedSFX);

        gameObject.SetActive(false);
        Destroy(this);
    }
    
    protected void PlayAudioGlobally(AudioClip audioClip) {
        Debug.Log(tag+" GlobalAudioSource is playing");
        GlobalAudioSource.clip = audioClip;
        GlobalAudioSource.Play();
    }
}