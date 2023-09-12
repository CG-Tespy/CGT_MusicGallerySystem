using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SongInfoType = CGT.MusicGallery.SongEntry.InfoType;
using System.Text;
using System.Text.RegularExpressions;

namespace CGT.MusicGallery
{
    /// <summary>
    /// For displaying text from a SongEntry
    /// </summary>
    public class SongTextView : SongView
    {
        [SerializeField] protected TextMeshProUGUI textField;
        [SerializeField] protected SongInfoType whatToDisplay = SongInfoType.Null;
        [SerializeField] protected bool removeNewlines;
        
        public virtual SongInfoType WhatToDisplay { get { return whatToDisplay; } }

        protected virtual void Awake()
        {
            if (textField == null)
                textField = GetComponent<TextMeshProUGUI>();
        }

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
            RemoveNewlinesAsNeeded();
        }

        protected IDictionary<SongInfoType, string> valueCache = new Dictionary<SongInfoType, string>();

        protected virtual string ArtistNamesFormatted()
        {
            var artists = Song.Artists;
            if (artists.Count <= 0)
                return "";

            stringBuilder.Clear();
            stringBuilder.Append(artists[0]);

            for (int i = 1; i < artists.Count; i++)
            {
                string artistName = artists[i];
                stringBuilder.Append($", {artistName}");
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
        
        protected virtual void RemoveNewlinesAsNeeded()
        {
            if (!removeNewlines)
                return;

            // Can't just iterate over the dictionary; in Unity, it doesn't support ElementAt(num).
            // And foreach loops don't let you change the values, so...
            valueCache[SongInfoType.LockComments] = valueCache[SongInfoType.LockComments].Replace("\n", " ");
            valueCache[SongInfoType.LockComments] = doubleSpaces.Replace(valueCache[SongInfoType.LockComments], " ");

            valueCache[SongInfoType.UnlockComments] = valueCache[SongInfoType.UnlockComments].Replace('\n', ' ');
            valueCache[SongInfoType.UnlockComments] = doubleSpaces.Replace(valueCache[SongInfoType.UnlockComments], " ");

            valueCache[SongInfoType.LockName] = valueCache[SongInfoType.LockName].Replace("\n", " ");
            valueCache[SongInfoType.LockName] = doubleSpaces.Replace(valueCache[SongInfoType.LockName], " ");

            valueCache[SongInfoType.UnlockName] = valueCache[SongInfoType.UnlockName].Replace("\n", " ");
            valueCache[SongInfoType.UnlockName] = doubleSpaces.Replace(valueCache[SongInfoType.UnlockName], " ");

            valueCache[SongInfoType.LockBasedName] = valueCache[SongInfoType.LockBasedName].Replace("\n", " ");
            valueCache[SongInfoType.LockBasedName] = doubleSpaces.Replace(valueCache[SongInfoType.LockBasedName], " ");

            valueCache[SongInfoType.LockBasedComments] = valueCache[SongInfoType.LockBasedComments].Replace("\n", " ");
            valueCache[SongInfoType.LockBasedComments] = doubleSpaces.Replace(valueCache[SongInfoType.LockBasedComments], " ");
        }

        protected Regex doubleSpaces = new Regex(@"\s\s+", RegexOptions.Multiline);
    }
}