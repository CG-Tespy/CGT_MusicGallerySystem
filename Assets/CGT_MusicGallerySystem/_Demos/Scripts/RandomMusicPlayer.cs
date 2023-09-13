using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    [SerializeField] protected AudioClip[] songs;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected bool playOnAwake;

    protected virtual void Awake()
    {
        if (playOnAwake)
            Play();
    }

    public virtual void Play()
    {
        int index = Random.Range(0, songs.Length);

        AudioClip toPlay = songs[index];
        audioSource.Stop();
        audioSource.clip = toPlay;
        audioSource.Play();
    }
}
