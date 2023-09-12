using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.MusicGallery
{
    /// <summary>
    /// Sets all the views registered with it to always be linked to 
    /// the song this system is playing at the time.
    /// </summary>
    public class SetToCurrentlyPlaying : MonoBehaviour
    {
        [SerializeField] protected MusicPlayer musicPlayer;
        [SerializeField] protected SongView[] views = new SongView[] { };

        public virtual IList<IView> Views { get { return views; } }

        protected virtual void Awake()
        {
            if (musicPlayer == null)
                musicPlayer = FindObjectOfType<MusicPlayer>();
        }

        protected virtual void Start()
        {
            MusicGallery.OnMusicPlay += SetTheViews;
        }

        protected virtual void OnEnable()
        {
            if (MusicGallery != null)
                MusicGallery.OnMusicPlay += SetTheViews;
        }

        protected virtual MusicGalleryManager MusicGallery { get { return MusicGalleryManager.Inst; } }

        protected virtual void SetTheViews(MusicPlayArgs args)
        {
            if (args.MusicPlayer != this.musicPlayer)
                return;

            foreach (var viewEl in views)
            {
                viewEl.Song = args.Song;
                viewEl.Refresh();
            }
        }

        protected virtual void OnDisable()
        {
            MusicGallery.OnMusicPlay -= SetTheViews;
        }
    }
}