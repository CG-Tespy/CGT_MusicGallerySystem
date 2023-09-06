using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CGT.MusicGallery
{
    public class Myceliaudio : MonoBehaviour
    {
        [SerializeField] protected SongEntry startingSong;
        [SerializeField] protected bool playStartingSong;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this.transform.root);
            PrepMusicPlayer();
        }

        protected virtual void PrepMusicPlayer()
        {
            GameObject musicPlayerGO = new GameObject("MusicPlayer", typeof(AudioSource));
            musicPlayerGO.transform.SetParent(this.transform);
            musicPlayer = musicPlayerGO.GetComponent<AudioSource>();
            musicPlayer.loop = true;
            musicPlayer.volume = 0.1f;
        }

        protected AudioSource musicPlayer;

        protected virtual void Start()
        {
            if (playStartingSong && startingSong != null)
            {
                SwitchToBGM(startingSong);
            }
        }

        protected virtual void SwitchToBGM(SongEntry songEntry)
        {
            musicPlayer.Stop();
            musicPlayer.clip = songEntry.AudioClip;
            musicPlayer.Play();
            OnMusicPlay.Invoke(songEntry);
        }

        public static System.Action<SongEntry> OnMusicPlay = delegate { };

        protected virtual void OnEnable()
        {
            SongButtonController.AnyClicked += OnSongButtonClicked;
        }

        protected virtual void OnSongButtonClicked(SongEntry songEntry)
        {
            if (!songEntry.IsLocked)
                return;

            SwitchToBGM(songEntry);
        }

        protected virtual void OnDisable()
        {
            SongButtonController.AnyClicked -= OnSongButtonClicked;
        }

    }
}