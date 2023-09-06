using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SongInfoType = CGT.MusicGallery.SongEntry.InfoType;
using System.Text;

namespace CGT.MusicGallery
{
    /// <summary>
    /// For displaying text from a SongEntry
    /// </summary>
    public class SongTextView : SongView
    {
        [SerializeField] protected TextMeshProUGUI textField;
        [SerializeField] protected SongInfoType whatToDisplay = SongInfoType.Null;
        
        public override void Refresh()
        {
            string textToDisplay = "";

            if (this.HasAnythingToDisplay)
            {
                UpdateValueCache();
                textToDisplay = valueCache[whatToDisplay];
            }

            textField.text = textToDisplay;
        }

        protected virtual bool HasAnythingToDisplay
        {
            get { return whatToDisplay != SongInfoType.Null && Song != null; }
        }

        protected virtual void UpdateValueCache()
        {
            valueCache[SongInfoType.Artists] = ArtistNamesFormatted();

            valueCache[SongInfoType.LockComments] = Song.LockComments;
            valueCache[SongInfoType.UnlockComments] = Song.UnlockComments;
            
            valueCache[SongInfoType.LockName] = Song.LockName;
            valueCache[SongInfoType.UnlockName] = Song.UnlockName;

            valueCache[SongInfoType.IsLocked] = Song.IsLocked.ToString();

            UpdateLockBasedValues();
        }

        protected IDictionary<SongInfoType, string> valueCache = new Dictionary<SongInfoType, string>();

        protected virtual string ArtistNamesFormatted()
        {
            stringBuilder.Clear();

            IList<string> artists = Song.Artists;
            for (int i = 0; i < artists.Count; i++)
            {
                string artistName = artists[i];
                stringBuilder.Append($"{artistName}");

                bool atLastName = i == artists.Count - 1;
                if (!atLastName)
                    stringBuilder.Append(", "); // Can't have a comma as the last char, after all
            }

            string formatted = stringBuilder.ToString();
            return formatted;
        }

        protected StringBuilder stringBuilder = new StringBuilder();

        protected virtual void UpdateLockBasedValues()
        {
            // As in, based on whether or not the song is locked
            if (Song.IsLocked)
            {
                valueCache[SongInfoType.LockBasedComments] = Song.LockComments;
                valueCache[SongInfoType.LockBasedName] = Song.LockName;
            }
            else
            {
                valueCache[SongInfoType.LockBasedComments] = Song.UnlockComments;
                valueCache[SongInfoType.LockBasedName] = Song.UnlockName;
            }
        }
    }
}