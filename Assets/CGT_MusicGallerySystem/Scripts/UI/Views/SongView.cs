using UnityEngine;

namespace CGT.MusicGallery
{
    public abstract class SongView : MonoBehaviour, IView
    {
        [TextArea(1, 5)]
        [SerializeField] protected string notes = string.Empty;
        [SerializeField] protected SongEntry song;

        public virtual string Notes
        {
            get { return notes; }
        }

        public virtual SongEntry Song
        {
            get { return song; }
            set { song = value; }
        }

        public abstract void Refresh();

    }
}