using UnityEngine;
using System.Collections.Generic;

namespace CGT.MusicGallery
{
    public class SongButtonManager : MonoBehaviour
    {
        [SerializeField] protected RectTransform buttonHolder;
        [Tooltip("Whether or not to show locked entries")]
        [SerializeField] protected bool showLocked = true;

        public virtual void Init(IList<SongEntry> songs)
        {
            this.Songs = songs;
            SpawnButtons();
        }

        public virtual IList<SongEntry> Songs
        {
            get { return songs; }
            set
            {
                songs.Clear();
                songs.AddRange(value);
            }
        }

        protected List<SongEntry> songs = new List<SongEntry>();

        protected virtual void SpawnButtons()
        {
            foreach (var songEntry in songs)
            {
                SongButtonController newButton = Instantiate(songEntry.ButtonPrefab);
                newButton.transform.SetParent(buttonHolder);
                newButton.Song = songEntry;
                bool shouldBeShown = showLocked || !songEntry.IsLocked;
                newButton.gameObject.SetActive(shouldBeShown);

                Vector3 properScale = songEntry.ButtonPrefab.transform.localScale;
                newButton.transform.localScale = properScale;
                // ^ In case the buttons' scales would otherwise get messed up due to
                // being parented to the holder
            }
        }

    }
}