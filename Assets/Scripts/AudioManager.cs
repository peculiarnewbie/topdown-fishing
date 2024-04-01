using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip splashClip;
    [SerializeField] AudioClip castClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySplash()
    {
        audioSource.PlayOneShot(splashClip);
    }

    public void PlayCast()
    {
        audioSource.PlayOneShot(castClip);
    }
}
