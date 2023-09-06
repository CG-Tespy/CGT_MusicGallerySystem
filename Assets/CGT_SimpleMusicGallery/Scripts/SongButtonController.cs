using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ButtonClick = UnityEngine.UI.Button.ButtonClickedEvent;
using TMPro;

namespace CGT.MusicGallery
{
    public class SongButtonController : MonoBehaviour
    {
        [SerializeField] protected SongEntry song;
        [SerializeField] protected TextMeshProUGUI nameLabel;
        [SerializeField] protected Button button;

        public virtual SongEntry Song
        {
            get { return song; }
            set 
            { 
                song = value;
                UpdateNameDisplays();
            }
        }

        protected virtual void UpdateNameDisplays()
        {
            string nameToGoWith = string.Empty;
            if (song != null)
            {
                if (song.IsLocked)
                    nameToGoWith = song.UnlockName;
                else
                    nameToGoWith = song.LockName;

                this.gameObject.name = $"{song.name}_Button";
            }

            nameLabel.text = nameToGoWith;
        }
        
        protected virtual void OnEnable()
        {
            UpdateNameDisplays();
            OnClick.AddListener(ResponseToClick);
        }

        public ButtonClick OnClick
        {
            get { return button.onClick; }
        }

        protected virtual void ResponseToClick()
        {
            UpdateNameDisplays();
            AnyClicked.Invoke(song);
        }

        public static System.Action<SongEntry> AnyClicked = delegate { };

        protected virtual void OnDisable()
        {
            OnClick.RemoveListener(ResponseToClick);
        }
    }
}