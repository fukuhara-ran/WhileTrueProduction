using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SfxSource;

    [Header("------- Audio Clip -------")]
    public AudioClip soundtrack;


    // Start is called before the first frame update
    void Start()
    {
        MusicSource.clip = soundtrack;
        MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
