using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace CGT.MusicGallery
{
    [CreateAssetMenu(fileName = "NewSongEntry", menuName = "Myceliaudio/SongEntry")]
    public class SongEntry : ScriptableObject
    {
        [Tooltip("The names of the people who made the song")]
        [SerializeField] protected string[] artists = new string[] { };

        [Header("Comments")]

        [Tooltip("Comments for when this is UNlocked")]
        [TextArea(3, 10)]
        [SerializeField] protected string unlockComments = string.Empty;

        [Tooltip("Comments for when this is locked")]
        [TextArea(3, 10)]
        [SerializeField] protected string lockComments = string.Empty;

        [Header("Display Names")]

        [Tooltip("Name shown when UNlocked")]
        [TextArea(1, 6)]
        [SerializeField] protected string unlockName = string.Empty;

        [Tooltip("Name shown when locked")]
        [TextArea(1, 6)]
        [SerializeField] protected string lockName = string.Empty;

        [SerializeField] protected bool isLocked;
        [SerializeField] protected AudioClip audioClip;
        
        [SerializeField] protected SongButtonController buttonPrefab;

        /// <summary>
        /// The names of those who made the song
        /// </summary>
        public virtual IList<string> Artists { get { return artists; } }
        public virtual string UnlockComments { get { return unlockComments; } }
        public virtual string LockComments { get { return lockComments; } }

        /// <summary>
        /// For when this song is UNlocked
        /// </summary>
        public virtual string UnlockName { get { return unlockName; } }

        /// <summary>
        /// For when this song is locked
        /// </summary>
        public virtual string LockName { get { return lockName; } }

        public virtual bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        public virtual AudioClip AudioClip { get { return audioClip; } }
        public virtual SongButtonController ButtonPrefab { get { return buttonPrefab; } }
        
        public enum InfoType
        {
            Null,

            Artists,

            LockBasedComments,
            UnlockComments,
            LockComments,

            LockBasedName,
            UnlockName,
            LockName,

            IsLocked,
        }

    }
}