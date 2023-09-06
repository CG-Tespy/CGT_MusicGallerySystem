using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.MusicGallery
{
    public class SongButtonManager : MonoBehaviour
    {
        [SerializeField] protected SongEntry[] songs = new SongEntry[] { };
        [SerializeField] protected RectTransform buttonHolder;
        [Tooltip("Whether or not to show disabled entries")]
        [SerializeField] protected bool showDisabled;

        protected virtual void Awake()
        {
            SpawnButtons();
        }

        protected virtual void SpawnButtons()
        {
            foreach (var songEntry in songs)
            {
                SongButtonController newButton = Instantiate(songEntry.ButtonPrefab);
                newButton.transform.SetParent(buttonHolder);
                newButton.Song = songEntry;
                bool shouldBeShown = showDisabled || songEntry.IsLocked;
                newButton.gameObject.SetActive(shouldBeShown);
            }
        }


    }
}