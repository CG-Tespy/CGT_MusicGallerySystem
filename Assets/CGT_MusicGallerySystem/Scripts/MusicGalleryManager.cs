using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.MusicGallery
{
    /// <summary>
    /// The top-level script that manages the system as a whole while being the main
    /// interface clients are meant to deal with.
    /// </summary>
    public class MusicGalleryManager : MonoBehaviour
    {
        [Tooltip("Entries that will be loaded from the specified subfolders below")]
        [SerializeField] protected List<SongEntry> songs = new List<SongEntry>();

        [Tooltip("Names of the folders within the Resources folder to load SongEntries from")]
        [SerializeField] protected string[] folderNames = new string[] { "SongEntries" };

        public virtual IList<string> FolderNames { get { return folderNames; } }

        protected virtual void Awake()
        {
            bool otherAlreadyExists = Inst != null && Inst != this;
            if (otherAlreadyExists)
            {
                Destroy(this.gameObject);
                return;
            }

            Inst = this;
            FetchSubmodules();
        }

        public static MusicGalleryManager Inst { get; protected set; }

        protected virtual void FetchSubmodules()
        {
            var musicPlayersFound = GetComponentsInChildren<MusicPlayer>();
            var buttonManagersFound = GetComponentsInChildren<SongButtonManager>();

            musicPlayers.AddRange(musicPlayersFound);
            buttonManagers.AddRange(buttonManagersFound);
        }

        protected List<MusicPlayer> musicPlayers = new List<MusicPlayer>();
        protected List<SongButtonManager> buttonManagers = new List<SongButtonManager>();

        protected virtual void OnEnable()
        {
            foreach (var player in musicPlayers)
            {
                player.OnMusicPlay += ResponseToMusicPlaying;
            }
        }

        protected virtual void ResponseToMusicPlaying(MusicPlayArgs args)
        {
            OnMusicPlay.Invoke(args);
        }

        protected virtual void OnDisable()
        {
            foreach (var player in musicPlayers)
            {
                player.OnMusicPlay -= ResponseToMusicPlaying;
            }
        }

        protected virtual void Start()
        {
            songs.Clear(); // We want to ignore whatever's set in the Inspector
            FetchSongEntries();
            InitSubmodules();
        }

        protected virtual void FetchSongEntries()
        {
            IList<SongEntry> songsFound;
            foreach (var folder in folderNames)
            {
                songsFound = Resources.LoadAll<SongEntry>(folder);
                songs.AddRange(songsFound);
            }

            songs.Sort((firstSong, secondSong) => firstSong.OrderInList.CompareTo(secondSong.OrderInList));
        }

        protected virtual void InitSubmodules()
        {
            foreach (var player in musicPlayers)
            {
                player.Init();
            }

            foreach (var manager in buttonManagers)
            {
                manager.Init(songs);
            }
        }

        public event MusicPlayEvent OnMusicPlay = delegate { };

    }
}