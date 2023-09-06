using UnityEngine;

namespace CGT.MusicGallery
{
    public abstract class SongView : MonoBehaviour, IView
    {
        [SerializeField] protected SongEntry song;
        public virtual SongEntry Song
        {
            get { return song; }
            set { song = value; }
        }

        public abstract void Refresh();

    }
}