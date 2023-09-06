using UnityEngine;

namespace CGT.MusicGallery
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] protected SongEntry startingSong;
        [SerializeField] protected bool playStartingSong;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected AudioSource audioSource;

        public virtual void Init()
        {
            if (playStartingSong && startingSong != null)
            {
                SwitchToBGM(startingSong);
            }
        }

        protected virtual void SwitchToBGM(SongEntry songEntry)
        {
            audioSource.Stop();
            audioSource.clip = songEntry.AudioClip;
            audioSource.Play();

            MusicPlayArgs args = new MusicPlayArgs()
            {
                Song = songEntry,
                MusicPlayer = this
            };
            OnMusicPlay.Invoke(args);
        }

        public event MusicPlayEvent OnMusicPlay = delegate { };

        protected virtual void OnEnable()
        {
            SongButtonController.AnyClicked += OnSongButtonClicked;
        }

        protected virtual void OnSongButtonClicked(SongEntry songEntry)
        {
            if (songEntry.IsLocked)
                return;

            SwitchToBGM(songEntry);
        }

        protected virtual void OnDisable()
        {
            SongButtonController.AnyClicked -= OnSongButtonClicked;
        }

        /// <summary>
        /// On a scale of 0 (silent) to 100 (loudest)
        /// </summary>
        public virtual float Volume
        {
            get { return audioSource.volume * 100f; }
            set { audioSource.volume = value / 100f; }
        }

    }
}