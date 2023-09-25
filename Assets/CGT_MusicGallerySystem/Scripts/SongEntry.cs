using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace CGT.MusicGallery
{
    [CreateAssetMenu(fileName = "NewSongEntry", menuName = "MusicGallery/SongEntry")]
    public class SongEntry : ScriptableObject
    {
        [SerializeField] protected bool isLocked;

        [Tooltip("Affects how far up the list this will show up. Lower = higher on the list")]
        [SerializeField] protected int orderInList;

        [Tooltip("The names of the people who made the song")]
        [SerializeField] protected string[] artists = new string[] { };

        [Header("Comments")]

        [Tooltip("Comments for when this is UNlocked")]
        [TextArea(3, 10)]
        [SerializeField] protected string unlockComments = string.Empty;

        [Tooltip("Comments for when this is locked")]
        [TextArea(3, 10)]
        [SerializeField] protected string lockComments = "???";

        [Header("Display Names")]

        [Tooltip("Name shown when UNlocked")]
        [TextArea(1, 6)]
        [SerializeField] protected string unlockName = string.Empty;

        [Tooltip("Name shown when locked")]
        [TextArea(1, 6)]
        [SerializeField] protected string lockName = "???";

        [SerializeField] protected AudioClip audioClip;
        
        [SerializeField] protected SongButtonController buttonPrefab;

        public virtual int OrderInList { get { return orderInList; } }

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